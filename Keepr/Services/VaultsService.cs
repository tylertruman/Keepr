using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vaultsRepo;
    private readonly KeepsRepository _keepsRepo;
    public VaultsService(VaultsRepository vaultsRepo, KeepsRepository keepsRepo)
    {
      _vaultsRepo = vaultsRepo;
      _keepsRepo = keepsRepo;
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