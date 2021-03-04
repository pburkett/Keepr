using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr_server.Models;

namespace keepr_server.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile GetById(string id)
    {
      string sql = "SELECT * FROM profiles WHERE id = @id";
      return _db.QueryFirstOrDefault<Profile>(sql, new { id });
    }


    internal IEnumerable<ProfilePartyMemberViewModel> GetByPartyId(int id)
    {
      string sql = @"
      SELECT
      prof.*
      pm.id as PartyMemberId
      FROM partymembers pm
      JOIN profiles prof ON pm.memberId = prof.id
      WHERE partyId = @id
      ";
      return _db.Query<ProfilePartyMemberViewModel>(sql, new { id });
    }

    internal Profile Create(Profile newProfile)
    {
      string sql = @"
            INSERT INTO profiles
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)";
      _db.Execute(sql, newProfile);
      return newProfile;
    }
  }
}