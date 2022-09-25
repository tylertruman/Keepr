using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;
    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }
    internal Profile GetOne(string id)
    {
      string sql = @"
      SELECT * FROM accounts WHERE id = @id;";
      return _db.QueryFirstOrDefault<Profile>(sql, new { id });
    }
  }
}