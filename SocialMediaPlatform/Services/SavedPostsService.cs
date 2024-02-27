using SocialMediaPlatform.Configurations;
using SocialMediaPlatform.Extensions;
using SocialMediaPlatform.Helpers;
using SocialMediaPlatform.Interfaces;
using SocialMediaPlatform.Models.SavedPosts;

namespace SocialMediaPlatform.Services;

public class SavedPostsService : ISavedPostsService
{
    private List<SavedPost> savedPosts;
    private readonly UserPostsService userPostsService;
    private readonly UserAccountService userAccountService;
    public SavedPostsService(UserPostsService userPostsService,UserAccountService userAccountService)
    {
        this.userPostsService = userPostsService;
        this.userAccountService = userAccountService;
    }
    public async ValueTask<SavedPostViewModel> CreateAsync(SavedPostCreationModel post)
    {
        var existUserAccount = await userAccountService.GetByIdAsync(post.UserAccountId);
        var existPosts = await userPostsService.GetByIdAsync(post.PostId);

        savedPosts = await FileIO.ReadAsync<SavedPost>(Constants.SAVED_POSTS_PATH);
        var existPost = savedPosts.FirstOrDefault(p => p.PostId == post.PostId && p.UserAccountId == post.UserAccountId);
        if (existPost != null)
            throw new Exception($"This post has already saved with id {existPost.Id}");

        var createdSavedPost = savedPosts.Create<SavedPost>(post.ToMapped());
        await FileIO.WriteAsync(Constants.SAVED_POSTS_PATH, savedPosts);

        return createdSavedPost.ToMapped();
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        savedPosts = await FileIO.ReadAsync<SavedPost>(Constants.SAVED_POSTS_PATH);
        var existSavedPosts = savedPosts.FirstOrDefault(s => s.Id == id && !s.IsDeleted)
            ?? throw new Exception($"This post is not found with this id {id}");

        existSavedPosts.IsDeleted = true;
        existSavedPosts.DeletedAt = DateTime.UtcNow;
        await FileIO.WriteAsync(Constants.SAVED_POSTS_PATH, savedPosts);

        return true;
    }

    public async ValueTask<IEnumerable<SavedPostViewModel>> GetByUserAccountIdAsync(long UserAccountId)
    {
        savedPosts = await FileIO.ReadAsync<SavedPost>(Constants.SAVED_POSTS_PATH);
        return savedPosts.Where(s => s.UserAccountId == UserAccountId && !s.IsDeleted).ToList().ToMapped();
    }

    public async ValueTask<SavedPostViewModel> GetByIdAsync(long id)
    {
        savedPosts = await FileIO.ReadAsync<SavedPost>(Constants.SAVED_POSTS_PATH);
        var existSavedPosts = savedPosts.FirstOrDefault(s => s.Id == id && !s.IsDeleted)
            ?? throw new Exception($"This post is not found with this id {id}");

        return existSavedPosts.ToMapped();
    }
}
