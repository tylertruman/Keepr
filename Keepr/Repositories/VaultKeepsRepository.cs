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

    internal List<VaultKeepViewModel> GetKeepsByVaultId(int vaultId)
    {
      string sql = @"
      SELECT
      vK.*,
      k.*,
      a.*
      FROM vaultKeeps vK
      JOIN keeps k ON vK.keepId = k.id
      JOIN accounts a ON k.creatorId = a.id
      WHERE vK.vaultId = @vaultId;";
      List<VaultKeepViewModel> keeps = _db.Query<VaultKeep, VaultKeepViewModel, Account, VaultKeepViewModel>(sql, (vK, k, a) => {
        k.Creator = a;
        k.VaultKeepId = vK.Id;
        return k;
      }, new { vaultId }).ToList();
      return keeps;
    }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      string sql = @"
      INSERT INTO vaultKeeps
      (creatorId, vaultId, keepId)
      VALUES
      (@creatorId, @vaultId, @keepId);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      newVaultKeep.Id = id;
      return newVaultKeep;
    }

    // internal int Create(VaultKeep newVaultKeep)
    // {
    //   string sql = @"
    //   INSERT INTO vaultKeeps
    //   (creatorId, vaultId, keepId)
    //   VALUES
    //   (@creatorId, @vaultId, @keepId);
    //   SELECT LAST_INSERT_ID();";
    //   int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
    //   newVaultKeep.Id = id;
    //   return id;
    // }

    // internal int Create(VaultKeep newVaultKeep)
    // {
    //   string sql = @"
    //   INSERT INTO vaultKeeps
    //   (creatorId, vaultId, keepId)
    //   VALUES
    //   (@creatorId, @vaultId, @keepId);
    //   SELECT LAST_INSERT_ID();";
    //   int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
    //   // newVaultKeep.Id = id;
    //   return id;
    // }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM vaultKeeps WHERE id = @id;";
      _db.Execute(sql, new { id });
    }
  }
}