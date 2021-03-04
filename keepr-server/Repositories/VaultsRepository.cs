using System;
using System.Collections.Generic;
using System.Data;
using keepr_server.Models;
using Dapper;
using System.Linq;

namespace keepr_server.Repositories
{
  public class VaultsRepository
  {
    public readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }
    //NOTE dotnet add package dapper - to be able to communicate with db
    public IEnumerable<Vault> GetAll()
    {
      string sql = @"
      SELECT 
      vault.*,
      profile.*
       FROM vaults vault
       JOIN profiles profile ON vault.creatorId = profile.id;";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => { vault.Creator = profile; return vault; }, splitOn: "id");
    }
    public IEnumerable<Vault> GetByProfile(string id)
    {
      string sql = @"
      SELECT 
      vault.*,
      profile.*
       FROM vaults vault
       JOIN profiles profile ON vault.creatorId = profile.id
       WHERE vault.creatorId = @id;";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => { vault.Creator = profile; return vault; }, new { id }, splitOn: "id");
      // REVIEW why didnt this work?? the last line seems good to me.
      // SELECT
      // vault.*,
      // profile.*
      //  FROM vaults vault
      //  JOIN profiles profile ON vault.creatorId = profile.id
      //  WHERE vault.creatorId = profile.Id;
    }



    internal Vault GetById(int id)
    {
      string sql = @"
      SELECT 
      vault.*,
      profile.*
       FROM vaults vault
       JOIN profiles profile ON vault.creatorId = profile.id
       WHERE vault.id = @id;";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => { vault.Creator = profile; return vault; }, new { id }, splitOn: "id").FirstOrDefault();
    }


    internal Vault Create(Vault newVault)
    {

      string sql = @"
            INSERT INTO vaults
            (name, description, creatorId, isPrivate, img)
            VALUES
            (@Name, @Description, @CreatorId, @IsPrivate, @Img);
            SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newVault);
      return GetById(id);
    }



    internal void Delete(Vault vault)
    {
      string sql = "DELETE FROM vaults WHERE id = @Id";
      _db.Execute(sql, vault);
    }

    internal Vault Edit(Vault original)
    // Add new properties after SET to allow multiple edits. These should be consistent with your service.
    {
      string sql = @"
        UPDATE vaults
        SET
            name = @Name,
            description = @Description,
            isPrivate = @IsPrivate
        WHERE id = @Id;
        SELECT * FROM vaults WHERE id = @Id;";
      return _db.QueryFirstOrDefault<Vault>(sql, original);
    }
  }
}