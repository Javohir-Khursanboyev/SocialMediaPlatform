namespace SocialMediaPlatform.Models.PostsLikes;

public class PostsLikeCreationModel
{
    public long PostId { get; set; }
    public DateTime Time { get; set; }
    public long BeholderId { get; set; }
}
