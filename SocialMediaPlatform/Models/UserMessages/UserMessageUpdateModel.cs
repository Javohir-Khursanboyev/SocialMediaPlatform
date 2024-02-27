using SocialMediaPlatform.Enums;

namespace SocialMediaPlatform.Models.UserMessages;

public class UserMessageUpdateModel
{
    public long UserAccountId { get; set; }
    public long InterlocutorId { get; set; }
    public string MessageContent { get; set; }
}
