namespace SocialMediaPlatform.Models.UserPosts;

public class UserPostViewModel
{
    public long Id { get; set; }
    public long UserAccountId { get; set; }
    public string Content { get; set; }
    public string Comment { get; set; }
    public long LikeCount { get; set; }
}
