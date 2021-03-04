using System;
using System.Collections.Generic;
using System.Data;
using keepr_server.Models;
using Dapper;

namespace keepr_server.Repositories
{
  public class VaultKeepsRepository
  {
    public readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    //NOTE dotnet add package dapper - to be able to communicate with db
    public IEnumerable<VaultKeep> GetAll()
    {
      string sql = "SELECT * FROM vaultKeeps;";
      return _db.Query<VaultKeep>(sql);
    }

    internal VaultKeep GetById(int id)
    {
      string sql = "SELECT * FROM vaultKeeps WHERE id = @id;";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }


    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      // TODO [epic=snippet-todos] Add all properties from the model, except for database-generated values.  
      // TODO [epic=snippet-todos] The snippets will handle ID generation, but not any other database-generated values.
      string sql = @"
            INSERT INTO vaultKeeps
            (creatorId, vaultId, keepId)
            VALUES
            (@CreatorId, @VaultId, @KeepId);
            SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      newVaultKeep.Id = id;
      return newVaultKeep;
    }



    internal int Delete(int id)
    {
      string sql = "DELETE FROM vaultKeeps WHERE id = @Id";
      _db.Execute(sql, new { id });
      return id;
    }
  }
}