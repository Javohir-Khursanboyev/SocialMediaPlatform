using SocialMediaPlatform.Configurations;
using SocialMediaPlatform.Extensions;
using SocialMediaPlatform.Helpers;
using SocialMediaPlatform.Interfaces;
using SocialMediaPlatform.Models.PostsComments;

namespace SocialMediaPlatform.Services;

public class PostCommentService : IPostsCommentService
{
    private List<PostsComment> postsComments;
    private UserPostsService userPostsService;
    private UserAccountService userAccountService;
    public PostCommentService(UserPostsService userPostsService, UserAccountService userAccountService)
    {
        this.userPostsService = userPostsService;
        this.userAccountService = userAccountService;
    }
    public async ValueTask<PostsCommentViewModel> CreateAsync(PostsCommentCreationModel comment)
    {
        var existUserAccount = await userAccountService.GetByIdAsync(comment.BeholderId);
        var existPost =await userPostsService.GetByIdAsync(comment.PostId);

        postsComments = await FileIO.ReadAsync<PostsComment>(Constants.POSTS_COMMENTS_PATH);
        var createdPostsComments = postsComments.Create<PostsComment>(comment.ToMapped());
        createdPostsComments.Time = DateTime.UtcNow;
        await FileIO.WriteAsync(Constants.POSTS_COMMENTS_PATH, postsComments);

        return createdPostsComments.ToMapped();
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        postsComments = await FileIO.ReadAsync<PostsComment>(Constants.POSTS_COMMENTS_PATH);
        var existPostsComment = postsComments.FirstOrDefault(p => p.Id == id && !p.IsDeleted)
            ?? throw new Exception($"This comment is not found with Id {id}");

        existPostsComment.IsDeleted = true;
        existPostsComment.DeletedAt = DateTime.UtcNow;
        await FileIO.WriteAsync(Constants.POSTS_COMMENTS_PATH, postsComments);

        return true;
    }

    public async ValueTask<PostsCommentViewModel> GetByIdAsync(long id)
    {
        postsComments = await FileIO.ReadAsync<PostsComment>(Constants.POSTS_COMMENTS_PATH);
        var existPostsComment = postsComments.FirstOrDefault(p => p.Id == id && !p.IsDeleted)
            ?? throw new Exception($"This comment is not found with Id {id}");

        return existPostsComment.ToMapped();
    }

    public async ValueTask<IEnumerable<PostsCommentViewModel>> GetByPostIdAsync(long postId)
    {
        postsComments = await FileIO.ReadAsync<PostsComment>(Constants.POSTS_COMMENTS_PATH);
        return postsComments.Where(c => c.PostId == postId && !c.IsDeleted).ToList().ToMapped();
    }

    public async ValueTask<PostsCommentViewModel> UpdateAsync(long id, PostsCommentUpdateModel comment)
    {
        var existUserAccount = await userAccountService.GetByIdAsync(comment.BeholderId);
        var existPost = await userPostsService.GetByIdAsync(comment.PostId);

        postsComments = await FileIO.ReadAsync<PostsComment>(Constants.POSTS_COMMENTS_PATH);
        postsComments = await FileIO.ReadAsync<PostsComment>(Constants.POSTS_COMMENTS_PATH);
        var existPostsComment = postsComments.FirstOrDefault(p => p.Id == id && !p.IsDeleted)
            ?? throw new Exception($"This comment is not found with Id {id}");

        existPostsComment.PostId = comment.PostId;
        existPostsComment.UpdatedAt = DateTime.UtcNow;
        existPostsComment.BeholderId = comment.BeholderId;
        existPostsComment.CommentContent = comment.CommentContent;
        await FileIO.WriteAsync(Constants.POSTS_COMMENTS_PATH, postsComments);

        return existPostsComment.ToMapped();
    }
}
