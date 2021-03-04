using System.ComponentModel.DataAnnotations;

namespace keepr_server.Models
{
  public class VaultKeep
  {
    public VaultKeep(int id, string creatorId, int vaultId, int keepId)
    {
      this.Id = id;
      this.CreatorId = creatorId;
      this.VaultId = vaultId;
      this.KeepId = keepId;

    }
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public int VaultId { get; set; }
    public int KeepId { get; set; }

    public VaultKeep()
    {

    }
  }
}