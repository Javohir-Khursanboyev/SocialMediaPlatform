using SocialMediaPlatform.Models.Commons;

namespace SocialMediaPlatform.Models.PostsLikes;

public class PostsLike:Auditable
{
    public long PostId { get; set; }
    public long BeholderId { get; set; }
    public DateTime Time { get; set; }
}
