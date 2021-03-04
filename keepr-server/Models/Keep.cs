using System.ComponentModel.DataAnnotations;

namespace keepr_server.Models
{
  public class Keep
  {
    public Keep(string name, string description, string img, int views, int shares, int keeps, string creatorId, int id, Profile creator)
    {
      this.Name = name;
      this.Description = description;
      this.Img = img;
      this.Views = views;
      this.Shares = shares;
      this.Keeps = keeps;
      this.CreatorId = creatorId;
      this.Id = id;
      this.Creator = creator;

    }
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }
    [Required]

    public string Img { get; set; }
    public int Views { get; set; }
    public int Shares { get; set; }
    public int Keeps { get; set; }
    public int Id { get; set; }
    public string CreatorId { get; set; }

    public Profile Creator { get; set; }



    public Keep()
    {

    }
  }
  public class KeepVaultKeepViewModel : Keep
  {
    public int VaultKeepId { get; set; }
  }
}