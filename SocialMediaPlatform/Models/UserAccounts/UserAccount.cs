using SocialMediaPlatform.Enums;
using SocialMediaPlatform.Models.Commons;

namespace SocialMediaPlatform.Models.UserAccounts;

public class UserAccount:Auditable
{
    public long UserId { get; set; }
    public string NickName { get; set; }
    public string Bio {  get; set; }
    public Privacy Privacy { get; set; }
}
