using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vaultKeepsRepo;
    private readonly VaultsService _vaultsService;
    private readonly KeepsService _keepsService;
    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepo, VaultsService vaultsService, KeepsService keepsService)
    {
      _vaultKeepsRepo = vaultKeepsRepo;
      _vaultsService = vaultsService;
      _keepsService = keepsService;
    }

    internal List<VaultKeep> GetAll()
    {
      return _vaultKeepsRepo.GetAll();
    }
    internal VaultKeep GetOne(int id)
    {
      VaultKeep vaultKeep = _vaultKeepsRepo.GetOne(id);
      if(vaultKeep == null)
      {
        throw new Exception("No vault keep by that ID");
      }
      return vaultKeep;
    }
    internal VaultKeepViewModel Create(VaultKeep newVaultKeep, string userId)
    {
      Vault vault = _vaultsService.GetOne(newVaultKeep.VaultId, userId);
      if(vault.CreatorId != userId)
      {
        throw new Exception("You are not authorized");
      }
      int id = _vaultKeepsRepo.Create(newVaultKeep);
      VaultKeepViewModel vaultKeep = _keepsService.GetViewModelById(newVaultKeep.KeepId);
      vaultKeep.VaultKeepId = id;
      return vaultKeep;
    }

    internal string Delete(int id, string userId)
    {
      VaultKeep original = GetOne(id);
      if (original.CreatorId != userId)
      {
        throw new Exception("You are not authorized to delete this vault keep.");
      }
      _vaultKeepsRepo.Delete(id);
      return "The vault keep has been deleted.";
    }

  }
}