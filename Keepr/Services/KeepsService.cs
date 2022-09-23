using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _keepsRepo;
    public KeepsService(KeepsRepository keepsRepo)
    {
      _keepsRepo = keepsRepo;
    }
    internal List<Keep> GetAll()
    {
      return _keepsRepo.GetAll();
    }

    internal Keep GetOne(int id)
    {
      Keep keep = _keepsRepo.GetOne(id);
      if (keep == null)
      {
        throw new Exception("no keep by that id");
      }
      return keep;
    }
    internal Keep Create(Keep newKeep)
    {
      return _keepsRepo.Create(newKeep);
    }

    internal Keep Update(Keep update, string userId)
    {
      Keep original = GetOne(update.Id);
      if (original.CreatorId != userId)
      {
        throw new Exception("You are not the creator of this Keep, you are not authorized.");
      }
      original.Name = update.Name ?? original.Name;
      original.Description = update.Description ?? original.Description;
      original.Img = update.Img ?? original.Img;
      return _keepsRepo.Update(original);
    }

    internal string Delete(int id, string userId)
    {
      Keep original = GetOne(id);
      if (original.CreatorId != userId)
      {
        throw new Exception("You are not the creator of this Keep, You are not authorized to delete it.");
      }
      _keepsRepo.Delete(id);
      return $"Keep {original.Name} has been deleted.";
    }
  }
}