using SocialMediaPlatform.Models.UserMessages;

namespace SocialMediaPlatform.Interfaces;

public interface IUserMessageService
{
    ValueTask<UserMessageViewModel> CreateAsync(UserMessageCreationModel message);
    ValueTask<UserMessageViewModel> UpdateAsync(long id, UserMessageUpdateModel message);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<UserMessageViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<UserMessageViewModel>> GetByUserAccountIdAsync(long userAccountId, long InterlocutorId);
    ValueTask<IEnumerable<UserMessageViewModel>> UnreadMessageAsync();
}
