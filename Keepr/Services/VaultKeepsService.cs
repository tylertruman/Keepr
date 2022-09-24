using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vaultKeepsRepo;
    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepo)
    {
      _vaultKeepsRepo = vaultKeepsRepo;
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
    internal VaultKeep Create(VaultKeep vaultKeepData)
    {
      return _vaultKeepsRepo.Create(vaultKeepData);
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