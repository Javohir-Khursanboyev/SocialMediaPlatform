using SocialMediaPlatform.Models.Commons;

namespace SocialMediaPlatform.Models.UserPosts;

public class UserPost:Auditable
{
    public long UserAccountId { get; set; }
    public string Content {  get; set; } 
    public string Comment { get; set; }
    public long LikeCount { get; set; }
    public DateTime Time { get; set; }
}
