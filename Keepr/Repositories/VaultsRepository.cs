using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Vault GetOne(int id)
    {
      string sql = @"
      SELECT
      vaults.*,
      a.*
      FROM vaults
      JOIN accounts a ON vaults.creatorId = a.id
      WHERE vaults.id = @id
      GROUP BY(vaults.id);";
      return _db.Query<Vault, Profile, Vault>(sql, (vaults, profile) => 
      {
        vaults.Creator = profile;
        return vaults;
      }, new { id }).FirstOrDefault();
    }

    internal List<Vault> GetVaultsByProfileId(string id)
    {
      string sql = @"
      SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.id
      WHERE v.creatorId = @id AND
      v.isPrivate = false;";
      return _db.Query<Vault, Profile, Vault>(sql, (v, profile) => {
        v.Creator = profile;
        return v;
      }, new { id }).ToList();
    }
    internal List<Vault> GetAccountVaults(string id)
    {
      string sql = @"
      SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.id
      WHERE v.creatorId = @id;";
      return _db.Query<Vault, Profile, Vault>(sql, (v, profile) => {
        v.Creator = profile;
        return v;
      }, new { id }).ToList();
    }
    internal Vault Create(Vault vaultData)
    {
      string sql = @"
      INSERT INTO vaults
      (name, description, isPrivate, creatorId)
      VALUES
      (@name, @description, @isPrivate, @creatorId);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, vaultData);
      vaultData.Id = id;
      return vaultData;
    }


    internal Vault Update(Vault update)
    {
      string sql = @"
      UPDATE vaults SET
      name = @name,
      description = @description,
      isPrivate = @isPrivate
      WHERE id = @id;";
      _db.Execute(sql, update);
      return update;
    }


    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM vaults WHERE id = @id;";
      _db.Execute(sql, new { id });
    }
  }
}