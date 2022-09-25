using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<VaultKeep> GetAll()
    {
      string sql = @"
      SELECT
      vaultKeeps.*,
      a.*
      FROM vaultKeeps
      JOIN accounts a ON a.id = vaultKeeps.creatorId;";
      return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vaultKeep, profile) => 
      {
        vaultKeep.Creator = profile;
        return vaultKeep;
      }).ToList();
    }

    internal VaultKeep GetOne(int id)
    {
      string sql = @"
      SELECT
      vaultKeeps.*,
      a.*
      FROM vaultKeeps
      JOIN accounts a ON a.id = vaultKeeps.creatorId
      WHERE vaultKeeps.id = @id;";
      return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vaultKeep, profile) => {
        vaultKeep.Creator = profile;
        return vaultKeep;
      }, new { id }).FirstOrDefault();
    }

    internal int Create(VaultKeep newVaultKeep)
    {
      string sql = @"
      INSERT INTO vaultKeeps
      (creatorId, vaultId, keepId)
      VALUES
      (@creatorId, @vaultId, @keepId);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      newVaultKeep.Id = id;
      return id;
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM vaultKeeps WHERE id = @id;";
      _db.Execute(sql, new { id });
    }
  }
}