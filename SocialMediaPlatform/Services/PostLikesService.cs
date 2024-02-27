using SocialMediaPlatform.Configurations;
using SocialMediaPlatform.Extensions;
using SocialMediaPlatform.Helpers;
using SocialMediaPlatform.Interfaces;
using SocialMediaPlatform.Models.PostsComments;
using SocialMediaPlatform.Models.PostsLikes;

namespace SocialMediaPlatform.Services;

public class PostLikesService : IPostsLikeService
{
    private List<PostsLike> postsLikes;
    private UserAccountService userAccountService;
    private UserPostsService userPostsService;
    public PostLikesService(UserAccountService userAccountService,UserPostsService userPostsService)
    {
        this.userAccountService = userAccountService;
        this.userPostsService = userPostsService;
    }
    public async ValueTask<PostLikeViewModel> CreateAsync(PostsLikeCreationModel like)
    {
        var existUserAccount = await userAccountService.GetByIdAsync(like.BeholderId);
        var existPost = await userPostsService.GetByIdAsync(like.PostId);

        postsLikes = await FileIO.ReadAsync<PostsLike>(Constants.POSTS_LIKE_PATH);
        var existPostLike = postsLikes.FirstOrDefault(l => l.PostId == like.PostId && l.BeholderId == like.BeholderId);
        if (existPostLike != null)
        {
            if (existPostLike.IsDeleted)
                return await UpdateAsync(existPostLike.Id,like);

            throw new Exception($"You have already liked this post ,Like id {existPostLike.Id}");
        }

        var createdPostLike = postsLikes.Create<PostsLike>(like.ToMapped());
        existPost.LikeCount += 1;
        await userPostsService.UpdateAsync(existPost.Id, existPost.ToMap(),true);
        await FileIO.WriteAsync(Constants.POSTS_LIKE_PATH, postsLikes);
        return createdPostLike.ToMapped();
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        postsLikes = await FileIO.ReadAsync<PostsLike>(Constants.POSTS_LIKE_PATH);
        var existPostLike = postsLikes.FirstOrDefault(l => l.Id == id && !l.IsDeleted)
            ?? throw new Exception($"This like is not found with Id {id}");

        existPostLike.IsDeleted = true;
        existPostLike.DeletedAt = DateTime.UtcNow;
        var existPost = await userPostsService.GetByIdAsync(existPostLike.PostId);
        existPost.LikeCount -= 1;
        await FileIO.WriteAsync(Constants.POSTS_LIKE_PATH, postsLikes);
        await userPostsService.UpdateAsync(existPost.Id, existPost.ToMap(), true);

        return true;
    }

    public async ValueTask<IEnumerable<PostLikeViewModel>> GetByPostIdAsync(long postId)
    {
        postsLikes = await FileIO.ReadAsync<PostsLike>(Constants.POSTS_LIKE_PATH);
        return postsLikes.Where(l => l.PostId == postId && !l.IsDeleted).ToList().ToMapped();
    }

    public async ValueTask<PostLikeViewModel> GetByIdAsync(long id)
    {
        postsLikes = await FileIO.ReadAsync<PostsLike>(Constants.POSTS_LIKE_PATH);
        var existPostLike = postsLikes.FirstOrDefault(l => l.Id == id && !l.IsDeleted)
            ?? throw new Exception($"This like is not found with Id {id}");

        return existPostLike.ToMapped();
    }
    private async ValueTask<PostLikeViewModel> UpdateAsync(long id, PostsLikeCreationModel like)
    {
        postsLikes = await FileIO.ReadAsync<PostsLike>(Constants.POSTS_LIKE_PATH);
        var existPostLike = postsLikes.FirstOrDefault(l => l.Id == id);
        existPostLike.IsDeleted = true;
        existPostLike.UpdatedAt = DateTime.UtcNow;
        await FileIO.WriteAsync(Constants.POSTS_COMMENTS_PATH, postsLikes);

        return existPostLike.ToMapped() ;
    }
}
