using System;
using System.Collections.Generic;
using keepr_server.Models;
using keepr_server.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr_server.Services
{
  public class VaultKeepsService
  {

    private readonly VaultKeepsRepository _repo;

    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    internal IEnumerable<VaultKeep> Get()
    {
      return _repo.GetAll();
    }

    internal VaultKeep Get(int id, string userId)
    {
      VaultKeep vaultKeep = _repo.GetById(id);
      if (vaultKeep == null)
      {
        throw new Exception("invalid Id");
      }
      return vaultKeep;
    }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      return _repo.Create(newVaultKeep);
    }



    internal VaultKeep Delete(int id, string userId)
    {
      VaultKeep vaultKeep = Get(id, userId);
      if (vaultKeep.CreatorId != userId)
      {
        throw new Exception("Access Denied, You cannot edit something that is not yours");
      }
      int deletedId = _repo.Delete(id);
      return vaultKeep;
    }
  }
}