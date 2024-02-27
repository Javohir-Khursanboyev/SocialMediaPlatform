using SocialMediaPlatform.Models.PostsComments;

namespace SocialMediaPlatform.Interfaces;

public interface IPostsCommentService
{
    ValueTask<PostsCommentViewModel> CreateAsync(PostsCommentCreationModel comment);
    ValueTask<PostsCommentViewModel> UpdateAsync(long id, PostsCommentUpdateModel comment);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<PostsCommentViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<PostsCommentViewModel>> GetByPostIdAsync(long postId);
}
