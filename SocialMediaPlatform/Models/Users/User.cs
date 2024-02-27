using SocialMediaPlatform.Models.Commons;

namespace SocialMediaPlatform.Models.Users;

public class User:Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
}
