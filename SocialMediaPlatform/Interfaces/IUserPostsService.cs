using SocialMediaPlatform.Models.UserPosts;

namespace SocialMediaPlatform.Interfaces;

public interface IUserPostsService
{
    ValueTask<UserPostViewModel> CreateAsync(UserPostCreationModel post);
    ValueTask<UserPostViewModel> UpdateAsync(long id, UserPostUpdateModel post, bool isUsesDeleted);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<UserPostViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<UserPostViewModel>> GetByUserAccountIdAsync(long userAccountId);
    ValueTask<IEnumerable<UserPostViewModel>> GetAllAsync();
}
