using SocialMediaPlatform.Enums;

namespace SocialMediaPlatform.Models.UserMessages;

public class UserMessageViewModel
{
    public long Id { get; set; }
    public long UserAccountId { get; set; }
    public long InterlocutorId { get; set; }
    public string MessageContent { get; set; }
    public DateTime Time { get; set; }
}
