namespace SocialMediaPlatform.Models.UserPosts;

public class UserPostCreationModel
{
    public long UserAccountId { get; set; }
    public string Content { get; set; }
    public string Comment { get; set; }
}
