using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using keepr_server.Models;
using keepr_server.Services;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;

namespace keepr_server.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _service;
    private readonly VaultsService _vService;
    private readonly KeepsService _kService;


    public ProfilesController(ProfilesService service, VaultsService vService, KeepsService kService)
    {
      _service = service;
      _vService = vService;
      _kService = kService;

    }


    [HttpGet("{id}")]
    public ActionResult<Profile> Get(string id)
    {
      try
      {
        Profile profile = _service.GetProfileById(id);
        return Ok(profile);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/vaults")]
    public async Task<ActionResult<IEnumerable<Vault>>> GetVaultsByProfile(string id)
    {
      try
      {
        try
        {
          Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
          return Ok(_vService.GetByProfile(id, userInfo.Id));
        }
        catch (System.Exception err)
        {
          return Ok(_vService.GetByProfile(id));

        }
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    [HttpGet("{id}/keeps")]
    public ActionResult<IEnumerable<Vault>> GetKeepsByProfile(string id)
    {
      try
      {
        return Ok(_kService.GetByProfile(id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }



  }
}