namespace SocialMediaPlatform.Models.PostsLikes;

public class PostLikeViewModel
{
    public long Id { get; set; }
    public long PostId { get; set; }
    public DateTime Time { get; set; }
    public long BeholderId { get; set; }
}
