using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using keepr_server.Models;
using Dapper;

namespace keepr_server.Repositories
{
  public class KeepsRepository
  {
    public readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    //NOTE dotnet add package dapper - to be able to communicate with db
    public IEnumerable<Keep> GetAll()
    {
      string sql = @"
      SELECT 
      keep.*,
      profile.*
       FROM keeps keep
       JOIN profiles profile ON keep.creatorId = profile.id;";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, splitOn: "id");
    }

    internal Keep GetById(int id)
    {
      string sql = @"
      SELECT 
      keep.*,
      profile.*
       FROM keeps keep
       JOIN profiles profile ON keep.creatorId = profile.id
       WHERE keep.id = @id;";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { id }, splitOn: "id").FirstOrDefault();

    }
    public IEnumerable<Keep> GetByVault(int id)
    {
      string sql = @"
      SELECT 
      keep.*,
      vk.id as VaultKeepId,
      profile.*
       FROM vaultkeeps vk
       JOIN keeps keep ON vk.keepId = keep.id
       JOIN profiles profile ON keep.creatorId = profile.id
       WHERE vaultId = @id;";
      return _db.Query<KeepVaultKeepViewModel, Profile, KeepVaultKeepViewModel>(sql, (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }, new { id }, splitOn: "id");
    }
    public IEnumerable<Keep> GetByProfile(string id)
    {
      string sql = @"
      SELECT 
      keep.*,
      profile.*
       FROM keeps keep
       JOIN profiles profile ON keep.creatorId = profile.id
       WHERE keep.creatorId = @id;";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { id }, splitOn: "id");
    }


    internal Keep Create(Keep newKeep)
    {
      string sql = @"
            INSERT INTO keeps
            (name, description, creatorId, img)
            VALUES
            (@Name, @Description, @CreatorId, @Img);
            SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newKeep);

      return GetById(id);
    }



    internal void Delete(Keep keep)
    {
      string sql = "DELETE FROM keeps WHERE id = @Id";
      _db.Execute(sql, keep);
    }

    internal Keep Edit(Keep original)
    // Add new properties after SET to allow multiple edits. These should be consistent with your service.
    {
      string sql = @"
        UPDATE keeps
        SET
            name = @Name,
            description = @Description,
            img = @Img,
            keeps = @Keeps,
            views = @Views
        WHERE id = @Id;
        SELECT * FROM keeps WHERE id = @Id;";
      return _db.QueryFirstOrDefault<Keep>(sql, original);
    }
  }
}