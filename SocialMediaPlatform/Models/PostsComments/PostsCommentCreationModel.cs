namespace SocialMediaPlatform.Models.PostsComments;

public class PostsCommentCreationModel
{
    public long PostId { get; set; }
    public long BeholderId { get; set; }
    public string CommentContent { get; set; }
}
