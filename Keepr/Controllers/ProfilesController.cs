using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _profilesService;
    public ProfilesController(ProfilesService profilesService)
    {
      _profilesService = profilesService;
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Profile>> GetOne(string id)
    {
      try
      {
      Account user = await HttpContext.GetUserInfoAsync<Account>();
      Profile profile = _profilesService.GetOne(id);
      return Ok(profile);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<List<Keep>>> GetKeeps(string id)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        List<Keep> keeps = _profilesService.GetKeeps(id);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/vaults")]
    public async Task<ActionResult<List<Vault>>> GetVaults(string id)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        List<Vault> vaults = _profilesService.GetVaults(id);
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}