using SocialMediaPlatform.Models.SavedPosts;

namespace SocialMediaPlatform.Interfaces;

public interface ISavedPostsService
{
    ValueTask<SavedPostViewModel> CreateAsync(SavedPostCreationModel user);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<SavedPostViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<SavedPostViewModel>> GetByUserAccountIdAsync(long userAccountId);
}
