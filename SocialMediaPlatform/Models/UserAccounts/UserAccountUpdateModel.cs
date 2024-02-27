using SocialMediaPlatform.Enums;

namespace SocialMediaPlatform.Models.UserAccounts;

public class UserAccountUpdateModel
{
    public long UserId { get; set; }
    public string NickName { get; set; }
    public string Bio { get; set; }
    public Privacy Privacy { get; set; }
}
