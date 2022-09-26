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
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vaultKeepsService;
    private readonly KeepsService _keepsService;
    public VaultKeepsController(VaultKeepsService vaultKeepsService, KeepsService keepsService)
    {
      _vaultKeepsService = vaultKeepsService;
      _keepsService = keepsService;
    }
    [HttpGet]
    public ActionResult<List<VaultKeep>> GetAll()
    {
      try
      {
        List<VaultKeep> vaultKeeps = _vaultKeepsService.GetAll();
        return Ok(vaultKeeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]
    public ActionResult<VaultKeep> GetOne(int id)
    {
      try
      {
        VaultKeep vaultKeep = _vaultKeepsService.GetOne(id);
        return Ok(vaultKeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // [HttpPost]
    // [Authorize]
    // public async Task<ActionResult<VaultKeepViewModel>> Create([FromBody] VaultKeep newVaultKeep)
    // {
    //   try
    //   {
    //     Account user = await HttpContext.GetUserInfoAsync<Account>();
    //     newVaultKeep.CreatorId = user.Id;
    //     newVaultKeep.Creator = user;
        
    //     VaultKeepViewModel keep = _vaultKeepsService.Create(newVaultKeep, user.Id);
    //     // keep.Creator = user;
    //     // keep.VaultKeepId = newVaultKeep.KeepId;
    //     return Ok(keep);
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVaultKeep)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        newVaultKeep.CreatorId = user.Id;
        newVaultKeep.Creator = user;
        VaultKeep vaultKeep = _vaultKeepsService.Create(newVaultKeep, user?.Id);
        // Keep keep = _keepsService.GetViewModelById(newVaultKeep.KeepId);
        // keep.Kept += 1;
        // VaultKeepViewModel keep = _vaultKeepsService.Create(newVaultKeep, user.Id);
        // keep.Creator = user;
        // keep.VaultKeepId = newVaultKeep.KeepId;
        return Ok(vaultKeep);
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
        string message = _vaultKeepsService.Delete(id, user.Id);
        return Ok(message);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}