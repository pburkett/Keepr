using System.ComponentModel.DataAnnotations;

namespace keepr_server.Models
{
  public class Vault
  {


    public int Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsPrivate { get; set; }
    public string CreatorId { get; set; }

    public Profile Creator { get; set; }

    public string Img { get; set; }

    public Vault()
    {

    }

    public Vault(int id, string name, string description, bool isPrivate, string creatorId, Profile creator, string img)
    {
      Id = id;
      Name = name;
      Description = description;
      IsPrivate = isPrivate;
      CreatorId = creatorId;
      Creator = creator;
      Img = img;
    }
  }
}