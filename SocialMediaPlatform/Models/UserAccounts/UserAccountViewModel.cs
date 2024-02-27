using SocialMediaPlatform.Enums;

namespace SocialMediaPlatform.Models.UserAccounts;

public class UserAccountViewModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string NickName { get; set; }
    public string Bio { get; set; }
    public Privacy Privacy { get; set; }
}
