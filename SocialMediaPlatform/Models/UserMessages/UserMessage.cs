using SocialMediaPlatform.Enums;
using SocialMediaPlatform.Models.Commons;

namespace SocialMediaPlatform.Models.UserMessages;

public class UserMessage:Auditable
{
    public long SenderAccountId { get; set; }
    public long InterlocutorId { get; set; }
    public string MessageContent { get; set; }
    public DateTime Time { get; set; }
    public bool IsRead { get; set; }
}
