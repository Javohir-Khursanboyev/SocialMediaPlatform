using SocialMediaPlatform.Models.PostsLikes;

namespace SocialMediaPlatform.Interfaces;

public interface IPostsLikeService
{
    ValueTask<PostLikeViewModel> CreateAsync(PostsLikeCreationModel like);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<PostLikeViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<PostLikeViewModel>> GetByPostIdAsync(long postId);
}
