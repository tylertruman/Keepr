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
    private readonly KeepsRepository _keepsRepo;
    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepo, VaultsService vaultsService, KeepsService keepsService, KeepsRepository keepsRepo)
    {
      _vaultKeepsRepo = vaultKeepsRepo;
      _vaultsService = vaultsService;
      _keepsService = keepsService;
      _keepsRepo = keepsRepo;
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
    // internal List<Keep> GetKeepsByVaultId(int id, string userId)
    // {
    //   Vault vault =  _vaultsRepo.GetById(id);
    //   if(vault.IsPrivate == true && vault.CreatorId == userId)
    //   {
    //     return _vaultKeepsRepo.GetKeepsByVaultId(vaultId);
    //   }
    // }
    // internal async List<VaultKeepViewModel> GetKeepsByVaultId(int vaultId)
    // {
    //   return _vaultKeepsRepo.GetKeepsByVaultId(vaultId);
    // }
    // internal VaultKeepViewModel Create(VaultKeep newVaultKeep, string userId)
    // {
    //   Vault vault = _vaultsService.GetById(newVaultKeep.VaultId);
    //   if(vault.CreatorId != userId)
    //   {
    //     throw new Exception("You are not authorized");
    //   }
    //   // NOTE not sure if this will work v
    //   // Keep keep = _keepsService.GetOne(newVaultKeep.KeepId);
      
    //   // v NOTE not sure if this will work.
    //   // newVaultKeep.VaultId = vault.Id; 
    //   // newVaultKeep.KeepId = keep.Id;
    //   int id = _vaultKeepsRepo.Create(newVaultKeep);
    //   VaultKeepViewModel vaultKeep = _keepsService.GetViewModelById(newVaultKeep.KeepId);
    //   // vaultKeep.VaultKeepId = id;
    //   // vaultKeep.Id = id; YO
    //   // return vaultKeep;
    //   // return vaultKeep; YO
    //   return vaultKeep;
    // }

    internal string Delete(int id, string userId)
    {
      VaultKeep original = GetOne(id);
      if (original.CreatorId != userId)
      {
        throw new Exception("You are not authorized to delete this vault keep.");
      }
      _vaultKeepsRepo.Delete(id);
      Keep keep = _keepsService.GetOne(original.KeepId);
      keep.Kept--;
      _keepsRepo.UpdateKept(keep);
      return "The vault keep has been deleted.";
    }

    internal VaultKeep Create(VaultKeep newVaultKeep, string userId)
    {
      Vault vault = _vaultsService.GetById(newVaultKeep.VaultId);
      if(vault.CreatorId != userId)
      {
        throw new Exception("You are not authorized to add a keep to a vault you don't own.");
      }
      Keep keep = _keepsService.GetOne(newVaultKeep.KeepId);
      keep.Kept++;
      _keepsRepo.UpdateKept(keep);
      // keep = _keepsRepo.UpdateKept(keep, keep.CreatorId);
      return _vaultKeepsRepo.Create(newVaultKeep);
    }
  }
}