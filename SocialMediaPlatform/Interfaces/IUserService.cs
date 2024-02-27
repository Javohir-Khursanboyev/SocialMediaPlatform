using SocialMediaPlatform.Models.Users;

namespace SocialMediaPlatform.Interfaces;

public interface IUserService
{
    ValueTask<UserViewModel> CreateAsync(UserCreationModel user);
    ValueTask<UserViewModel> UpdateAsync(long id, UserUpdateModel user, bool isUsesDeleted);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<UserViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<UserViewModel>> GetAllAsync();
}
