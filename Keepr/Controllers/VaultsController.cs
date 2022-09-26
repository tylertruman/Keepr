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
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vaultsService;
    private readonly VaultKeepsService _vaultKeepsService;
    public VaultsController(VaultsService vaultsService, VaultKeepsService vaultKeepsService)
    {
      _vaultsService = vaultsService;
      _vaultKeepsService = vaultKeepsService;
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> GetOne(int id)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        Vault vault = _vaultsService.GetOne(id, user?.Id);
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<List<VaultKeepViewModel>>> GetKeeps(int id)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        List<VaultKeepViewModel> keeps = _vaultsService.GetKeepsByVaultId(id, user?.Id);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // [HttpGet("{id}/keeps")]
    // public async ActionResult<List<VaultKeepViewModel>> GetKeeps(int id)
    // {
    //   try
    //   {
    //     // Account user = await HttpContext.GetUserInfoAsync<Account>();
    //     List<VaultKeepViewModel> keeps = _vaultKeepsService.GetKeepsByVaultId(id);
    //     return Ok(keeps);
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        vaultData.CreatorId = user.Id;
        Vault vault = _vaultsService.Create(vaultData);
        vault.Creator = user;
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Update(int id, [FromBody] Vault vaultData)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        vaultData.Id = id;
        Vault vault = _vaultsService.Update(vaultData, user);
        return Ok(vault);
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
        string message = _vaultsService.Delete(id, user);
        return Ok(message);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}