using SocialMediaPlatform.Models.Commons;

namespace SocialMediaPlatform.Models.PostsComments;

public class PostsComment:Auditable
{
    public long PostId { get; set; }
    public long BeholderId { get; set; }
    public string CommentContent { get; set; }
    public DateTime Time {  get; set; }
}
