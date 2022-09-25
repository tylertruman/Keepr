using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _profilesRepo;
    private readonly KeepsRepository _keepsRepo;
    private readonly VaultsRepository _vaultsRepo;
    public ProfilesService(ProfilesRepository profilesRepo, KeepsRepository keepsRepo, VaultsRepository vaultsRepo)
    {
      _profilesRepo = profilesRepo;
      _keepsRepo = keepsRepo;
      _vaultsRepo = vaultsRepo;
    }
    internal Profile GetOne(string id)
    {
      Profile profile = _profilesRepo.GetOne(id);
      if (profile == null)
      {
        throw new Exception("No profile by that ID.");
      }
      return profile;
    }

    internal List<Keep> GetKeeps(string id)
    {
      Profile profile = GetOne(id);
      return _keepsRepo.GetKeepsByProfileId(id);
    }

    internal List<Vault> GetVaults(string id)
    {
      Profile profile = GetOne(id);
      return _vaultsRepo.GetVaultsByProfileId(id);
    }
  }
}