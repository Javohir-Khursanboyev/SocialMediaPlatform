namespace SocialMediaPlatform.Models.PostsComments;

public class PostsCommentViewModel
{
    public long Id { get; set; }
    public long PostId { get; set; }
    public long BeholderId { get; set; }
    public string CommentContent { get; set; }
    public DateTime Time { get; set; }
}
