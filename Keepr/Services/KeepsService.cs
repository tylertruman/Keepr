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
  }
}