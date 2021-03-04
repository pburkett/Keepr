using System;
using System.Collections.Generic;
using keepr_server.Models;
using keepr_server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;

namespace keepr_server.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {

    private readonly VaultKeepsService _vaultKeepsService;
    private readonly KeepsService _keepsService;
    public VaultKeepsController(VaultKeepsService vaultKeepsService, KeepsService keepsService)
    {
      _vaultKeepsService = vaultKeepsService;
      _keepsService = keepsService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> Get(int id)
    {
      // REVIEW I'm not proud of this nested Try-Catch. What else can I do to deal with the eroor that line 33 throws when the user isnt logged in?
      try
      {
        try
        {
          Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
          return Ok(_vaultKeepsService.Get(id, userInfo.Id));
        }
        catch (System.Exception)
        { throw new Exception(); }
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPost]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVaultKeep)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newVaultKeep.CreatorId = userInfo.Id;

        Keep keep = _keepsService.Get(newVaultKeep.KeepId);
        _keepsService.AlterKeepsCount(newVaultKeep.KeepId, userInfo.Id, keep, 1);


        return Ok(_vaultKeepsService.Create(newVaultKeep));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<VaultKeep>> Delete(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        VaultKeep vaultKeep = _vaultKeepsService.Get(id, userInfo.Id);
        Keep keep = _keepsService.Get(vaultKeep.KeepId);

        _keepsService.AlterKeepsCount(id, userInfo.Id, keep, -1);

        return Ok(_vaultKeepsService.Delete(id, userInfo.Id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}
