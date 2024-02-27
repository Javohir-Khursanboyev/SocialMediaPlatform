using SocialMediaPlatform.Models.UserAccounts;

namespace SocialMediaPlatform.Interfaces;

public interface IUserAccountService
{
    ValueTask<UserAccountViewModel> CreateAsync(UserAccountCreationModel user);
    ValueTask<UserAccountViewModel> UpdateAsync(long id, UserAccountUpdateModel user, bool isUsesDeleted);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<UserAccountViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<UserAccountViewModel>> GetAllAsync();
}
