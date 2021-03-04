using System;
using System.Collections.Generic;
using keepr_server.Models;
using keepr_server.Services;
using Microsoft.AspNetCore.Mvc;
using CodeWorks.Auth0Provider;
using System.Threading.Tasks;


namespace keepr_server.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class KeepsController : ControllerBase
  {

    private readonly KeepsService _keepsService;
    public KeepsController(KeepsService keepsService)
    {
      _keepsService = keepsService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Keep>> Get()
    {
      try
      {
        return Ok(_keepsService.Get());
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Keep>> Get(int id)
    {
      try
      {
        Keep keep = _keepsService.Get(id);
        try
        {
          Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
          _keepsService.AlterViewsCount(keep.Id, userInfo.Id, keep, 1);


        }
        catch
        {
          _keepsService.AlterViewsCount(keep.Id, "", keep, 1);

        };

        return Ok(keep);

      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPost]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep newKeep)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newKeep.CreatorId = userInfo.Id;
        return Ok(_keepsService.Create(newKeep));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Keep>> Edit(int id, [FromBody] Keep editedKeep)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();

        editedKeep.Id = id;
        return Ok(_keepsService.Edit(editedKeep, userInfo.Id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Keep>> Delete(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_keepsService.Delete(id, userInfo.Id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}
