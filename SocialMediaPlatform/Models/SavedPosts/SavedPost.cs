using SocialMediaPlatform.Models.Commons;

namespace SocialMediaPlatform.Models.SavedPosts;

public class SavedPost:Auditable
{
    public long UserAccountId { get; set; }
    public long PostId { get; set; }
}
