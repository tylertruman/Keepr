using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
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
    public async Task<ActionResult<List<Keep>>> GetAll()
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        List<Keep> keeps = _keepsService.GetAll();
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Keep>> GetOne(int id)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        Keep keep = _keepsService.GetOne(id);
        keep.Views++;
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep newKeep)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        newKeep.CreatorId = user.Id;
        Keep keep = _keepsService.Create(newKeep);
        keep.Creator = user;
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Update(int id, [FromBody] Keep update)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        update.Id = id;
        Keep keep = _keepsService.Update(update, user.Id);
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        string message = _keepsService.Delete(id, user.Id);
        return Ok(message);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}