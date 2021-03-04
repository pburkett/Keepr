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
  public class VaultsController : ControllerBase
  {

    private readonly VaultsService _vaultsService;
    private readonly KeepsService _keepsService;

    public VaultsController(VaultsService vaultsService, KeepsService keepsService)
    {
      _vaultsService = vaultsService;
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
          return Ok(_vaultsService.Get(id, userInfo.Id));
        }
        catch (System.Exception)
        { return Ok(_vaultsService.Get(id)); }
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<IEnumerable<Vault>>> GetKeepsByVault(int id)
    {
      try
      {
        try
        {
          Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
          Vault vault = _vaultsService.Get(id, userInfo.Id);
          return Ok(_keepsService.GetByVault(id, userInfo.Id));
        }
        catch (System.Exception err)
        {
          Vault vault = _vaultsService.Get(id);
          return Ok(_keepsService.GetByVault(id));
        }

      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPost]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newVault.CreatorId = userInfo.Id;
        return Ok(_vaultsService.Create(newVault));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Vault>> Edit(int id, [FromBody] Vault editedVault)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        editedVault.Id = id;
        return Ok(_vaultsService.Edit(editedVault, userInfo.Id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Vault>> Delete(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_vaultsService.Delete(id, userInfo.Id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}
