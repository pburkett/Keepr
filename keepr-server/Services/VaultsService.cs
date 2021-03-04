using System;
using System.Collections.Generic;
using keepr_server.Models;
using keepr_server.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace keepr_server.Services
{
  public class VaultsService
  {

    private readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }

    internal IEnumerable<Vault> GetByProfile(string id, string userId = "")
    {
      IEnumerable<Vault> vaults = _repo.GetByProfile(id);
      if (id == userId)
      {
        return vaults;
      }
      return vaults.ToList().FindAll(r => !r.IsPrivate);
    }

    internal Vault Get(int id, string userId = "")
    {
      System.Console.WriteLine("hit vault service");
      Vault vault = _repo.GetById(id);
      if (vault == null)
      {
        throw new Exception("invalid Id");
      }
      if (vault.CreatorId != userId && vault.IsPrivate == true)
      {
        throw new Exception("Access Denied");
      }
      return vault;
    }

    internal Vault Create(Vault newVault)
    {
      return _repo.Create(newVault);
    }

    internal Vault Edit(Vault editVault, string userId)
    {
      Vault original = Get(editVault.Id, userId);
      if (original.CreatorId != userId)
      {
        throw new Exception("Access Denied, You cannot delete something that is not yours");
      }
      // Duplicate the following lwine with different properties to allow edits for multiple properties.
      original.Name = editVault.Name != null ? editVault.Name : original.Name;
      original.Description = editVault.Description != null ? editVault.Description : original.Description;
      original.IsPrivate = editVault.IsPrivate != null ? editVault.IsPrivate : original.IsPrivate;
      return _repo.Edit(original);
    }

    internal Vault Delete(int id, string userId)
    {
      Vault vault = Get(id, userId);
      if (vault.CreatorId != userId)
      {
        throw new Exception("Access Denied, You cannot delete something that is not yours");
      }
      _repo.Delete(vault);
      return vault;
    }
  }
}