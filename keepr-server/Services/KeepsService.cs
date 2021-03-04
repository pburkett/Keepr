using System;
using System.Collections.Generic;
using keepr_server.Models;
using keepr_server.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace keepr_server.Services
{
  public class KeepsService
  {

    private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }

    internal IEnumerable<Keep> Get()
    {
      return _repo.GetAll();
    }

    internal Keep Get(int id)
    {
      Keep keep = _repo.GetById(id);
      if (keep == null)
      {
        throw new Exception("invalid Id");
      }
      return keep;
    }
    internal IEnumerable<Keep> GetByVault(int id, string userId = "")
    {
      IEnumerable<Keep> keeps = _repo.GetByVault(id);
      return keeps;
    }
    internal IEnumerable<Keep> GetByProfile(string id)
    {
      IEnumerable<Keep> keeps = _repo.GetByProfile(id);

      return keeps;
    }


    internal Keep Create(Keep newKeep)
    {
      return _repo.Create(newKeep);
    }

    internal Keep Edit(Keep editKeep, string userId)
    {
      Keep original = Get(editKeep.Id);
      if (original.CreatorId != userId)
      {
        throw new Exception("Access Denied, You cannot edit something that is not yours");
      }
      // Duplicate the following lwine with different properties to allow edits for multiple properties.
      original.Name = editKeep.Name != null ? editKeep.Name : original.Name;
      original.Description = editKeep.Description != null ? editKeep.Description : original.Description;

      return _repo.Edit(original);
    }

    internal Keep AlterKeepsCount(int keepId, string userId, Keep keep, int amt)
    {
      // if (keep.CreatorId == userId)
      // {
      //   return keep;
      // }
      System.Console.WriteLine(keep.Keeps);

      keep.Keeps = keep.Keeps + amt;

      System.Console.WriteLine(keep.Keeps);

      return _repo.Edit(keep);

    }
    internal Keep AlterViewsCount(int keepId, string userId, Keep keep, int amt)
    {
      if (keep.CreatorId == userId)
      {
        return keep;
      }
      keep.Views = keep.Views + amt;
      return _repo.Edit(keep);
    }
    internal Keep Delete(int id, string userId)
    {
      Keep keep = Get(id);
      Keep original = Get(id);
      if (original.CreatorId != userId)
      {
        throw new Exception("Access Denied, You cannot delete something that is not yours");
      }
      _repo.Delete(keep);
      return keep;
    }
  }
}