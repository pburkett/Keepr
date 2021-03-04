namespace keepr_server.Models
{
  public class Profile
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Picture { get; set; }
  }
  // TODO rename this copy pasted junk u lazy bum
  public class ProfilePartyMemberViewModel : Profile
  {
    public int PartyMemberId { get; set; }
  }
}