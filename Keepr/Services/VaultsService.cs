using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vaultsRepo;
    private readonly KeepsRepository _keepsRepo;
    private readonly VaultKeepsRepository _vaultKeepsRepo;
    public VaultsService(VaultsRepository vaultsRepo, KeepsRepository keepsRepo, VaultKeepsRepository vaultKeepsRepo)
    {
      _vaultsRepo = vaultsRepo;
      _keepsRepo = keepsRepo;
      _vaultKeepsRepo = vaultKeepsRepo;
    }

    internal Vault GetOne(int id, string userId)
    {
      Vault vault = _vaultsRepo.GetOne(id);
      if(vault == null)
      {
        throw new Exception($"No vaults at that id: {id}");
      }
      if(vault.IsPrivate == true && vault.CreatorId != userId)
      {
        throw new Exception($"You are not authorized to access that private vault.");
      }
      return vault;
    }
    internal List<Keep> GetKeeps(int id, string userId)
    {
      Vault vault = GetOne(id, userId);
      return _keepsRepo.GetKeepsByVaultId(id);
    }

    internal Vault Create(Vault vaultData)
    {
      return _vaultsRepo.Create(vaultData);
    }

    internal Vault Update(Vault update, Account user)
    {
      Vault original = GetOne(update.Id, user.Id);
      if(original.CreatorId != user.Id)
      {
        throw new Exception("You are not authorized to edit this vault.");
      }
      original.Name = update.Name ?? original.Name;
      original.Description = update.Description ?? original.Description;
      original.IsPrivate = update.IsPrivate ?? original.IsPrivate;
      return _vaultsRepo.Update(original);
    }

    internal List<VaultKeepViewModel> GetKeepsByVaultId(int id, string userId)
    {
      Vault vault =  _vaultsRepo.GetById(id);
      if(vault.IsPrivate == true && vault.CreatorId == userId)
      {
        return _vaultKeepsRepo.GetKeepsByVaultId(id);
      } else if (vault.IsPrivate == false) 
      {
        return _vaultKeepsRepo.GetKeepsByVaultId(id);
      } else 
      {
        throw new Exception("You are not authorized to view the keeps in this vault.");
      }
    }

    internal Vault GetById(int id)
    {
      Vault vault = _vaultsRepo.GetById(id);
      if (vault == null)
      {
        throw new Exception("No vault by that ID.");
      }
      return vault;
    }

    internal string Delete(int id, Account user)
    {
      Vault original = GetOne(id, user.Id);
      if(original.CreatorId != user.Id)
      {
        throw new Exception("You are not authorized to delete this vault.");
      }
      _vaultsRepo.Delete(id);
      return $"The {original.Name} Vault has been deleted.";
    }
  }
}