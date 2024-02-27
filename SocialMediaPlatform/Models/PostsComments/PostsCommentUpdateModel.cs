namespace SocialMediaPlatform.Models.PostsComments;

public class PostsCommentUpdateModel
{
    public long PostId { get; set; }
    public long BeholderId { get; set; }
    public string CommentContent { get; set; }
}
