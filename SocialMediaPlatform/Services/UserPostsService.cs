using SocialMediaPlatform.Configurations;
using SocialMediaPlatform.Extensions;
using SocialMediaPlatform.Helpers;
using SocialMediaPlatform.Interfaces;
using SocialMediaPlatform.Models.UserPosts;

namespace SocialMediaPlatform.Services;

public class UserPostsService : IUserPostsService
{
    private List<UserPost> userPosts;
    private UserAccountService userAccountService;
    public UserPostsService(UserAccountService userAccountService)
    {
        this.userAccountService = userAccountService;
    }
    public async ValueTask<UserPostViewModel> CreateAsync(UserPostCreationModel post)
    {
        var existUserAccount = await userAccountService.GetByIdAsync(post.UserAccountId);
        userPosts = await FileIO.ReadAsync<UserPost>(Constants.USER_POSTS_PATH);
        var existUserPost = userPosts.FirstOrDefault(p => p.Content.ToLower() == post.Content.ToLower() && p.UserAccountId == post.UserAccountId);
        if (existUserPost != null)
        {
            if (existUserPost.IsDeleted)
                return await UpdateAsync(existUserPost.Id, post.ToMap(), true);

            throw new Exception($"This post is already exist With this content {post.Content}");
        }
        if (!File.Exists(post.Content))
            throw new Exception($"This .png file is not exist with this path {post.Content}");

            var createdUserPost = userPosts.Create<UserPost>(post.ToMapped());
        createdUserPost.Time = DateTime.UtcNow;
        await FileIO.WriteAsync(Constants.USER_POSTS_PATH, userPosts);
        return createdUserPost.ToMapped();
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        userPosts = await FileIO.ReadAsync<UserPost>(Constants.USER_POSTS_PATH);
        var existUserPost = userPosts.FirstOrDefault(p => p.Id == id && !p.IsDeleted)
            ?? throw new Exception($"This post is not found With this Id {id}");

        existUserPost.IsDeleted = true;
        existUserPost.DeletedAt = DateTime.UtcNow;
        await FileIO.WriteAsync(Constants.USER_POSTS_PATH, userPosts);

        return true;
    }

    public async ValueTask<IEnumerable<UserPostViewModel>> GetAllAsync()
    {
        userPosts = await FileIO.ReadAsync<UserPost>(Constants.USER_POSTS_PATH);
        return userPosts.Where(p => !p.IsDeleted).ToList().ToMapped();
    }

    public async ValueTask<UserPostViewModel> GetByIdAsync(long id)
    {
        userPosts = await FileIO.ReadAsync<UserPost>(Constants.USER_POSTS_PATH);
        var existUserPost = userPosts.FirstOrDefault(p => p.Id == id && !p.IsDeleted)
            ?? throw new Exception($"This post is not found With this Id {id}");

        return existUserPost.ToMapped();
    }

    public async ValueTask<IEnumerable<UserPostViewModel>> GetByUserAccountIdAsync(long userAccountId)
    {
        userPosts = await FileIO.ReadAsync<UserPost>(Constants.USER_POSTS_PATH);
        return userPosts.Where(p => p.UserAccountId == userAccountId && !p.IsDeleted).ToList().ToMapped();  
    }

    public async ValueTask<UserPostViewModel> UpdateAsync(long id, UserPostUpdateModel post, bool isUsesDeleted = false)
    {
        var existUserAccount = await userAccountService.GetByIdAsync(post.UserAccountId);
        userPosts = await FileIO.ReadAsync<UserPost>(Constants.USER_POSTS_PATH);
        var existUserPost = new UserPost();
        if (isUsesDeleted)
        {
            existUserPost = userPosts.FirstOrDefault(p => p.Id == id);
            existUserPost.IsDeleted = false;
            existUserPost.LikeCount = post.LikeCount;
        }
        else
            existUserPost = userPosts.FirstOrDefault(p => p.Id == id && !p.IsDeleted)
                ?? throw new Exception($"This post is not found With this Id {id}");

        if (!File.Exists(post.Content))
            throw new Exception($"This .png file is not exist with this path {post.Content}");

        existUserPost.Comment = post.Comment;
        existUserPost.Content = post.Content;
        existUserPost.UpdatedAt = DateTime.UtcNow;
        existUserPost.UserAccountId = post.UserAccountId;
        await FileIO.WriteAsync(Constants.USER_POSTS_PATH, userPosts);

        return existUserPost.ToMapped();
    }
}
