using SocialMediaPlatform.Configurations;
using SocialMediaPlatform.Extensions;
using SocialMediaPlatform.Helpers;
using SocialMediaPlatform.Interfaces;
using SocialMediaPlatform.Models.UserMessages;

namespace SocialMediaPlatform.Services;

public class UserMessageService : IUserMessageService
{
    private List<UserMessage> userMessages;
    private UserAccountService userAccountService;
    public UserMessageService(UserAccountService userAccountService)
    {
        this.userAccountService = userAccountService;
    }
    public async ValueTask<UserMessageViewModel> CreateAsync(UserMessageCreationModel message)
    {
        var senderUser = await userAccountService.GetByIdAsync(message.UserAccountId);
        var interlocutorUser = await userAccountService.GetByIdAsync(message.InterlocutorId);
        userMessages = await FileIO.ReadAsync<UserMessage>(Constants.USER_MESSAGE_PATH);
        var createdMessage = userMessages.Create<UserMessage>(message.ToMapped());
        createdMessage.Time = DateTime.UtcNow;
        createdMessage.IsRead = false;
        await FileIO.WriteAsync(Constants.USER_MESSAGE_PATH, userMessages);

        return createdMessage.ToMapped();
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        userMessages = await FileIO.ReadAsync<UserMessage>(Constants.USER_MESSAGE_PATH);
        var existUserMessage = userMessages.FirstOrDefault(x => x.Id == id && !x.IsDeleted)
            ?? throw new Exception($"This message is not found with this id{id}");

        existUserMessage.IsDeleted = true;
        existUserMessage.DeletedAt = DateTime.UtcNow;
        await FileIO.WriteAsync(Constants.USER_MESSAGE_PATH, userMessages);

        return true;
    }

    public async ValueTask<UserMessageViewModel> GetByIdAsync(long id)
    {
        userMessages = await FileIO.ReadAsync<UserMessage>(Constants.USER_MESSAGE_PATH);
        var existUserMessage = userMessages.FirstOrDefault(x => x.Id == id && !x.IsDeleted)
            ?? throw new Exception($"This message is not found with this id{id}");

        return existUserMessage.ToMapped();
    }

    public async ValueTask<IEnumerable<UserMessageViewModel>> GetByUserAccountIdAsync(long userAccountId, long InterlocutorId)
    {
        userMessages = await FileIO.ReadAsync<UserMessage>(Constants.USER_MESSAGE_PATH);
        var existSenderMessages = userMessages.Where
            (m => m.SenderAccountId == userAccountId && m.InterlocutorId == InterlocutorId && !m.IsDeleted).ToList().ToMapped();
        var existInterlocutorMessages = userMessages.Where
            (m => m.SenderAccountId == InterlocutorId && m.InterlocutorId == userAccountId && !m.IsDeleted).ToList().ToMapped();

        return existSenderMessages.Union(existInterlocutorMessages).ToList(); 
    }
    public ValueTask<IEnumerable<UserMessageViewModel>> UnreadMessageAsync()
    {
        throw new NotImplementedException();
    }

    public async ValueTask<UserMessageViewModel> UpdateAsync(long id, UserMessageUpdateModel message)
    {
        var senderUser = await userAccountService.GetByIdAsync(message.UserAccountId);
        var interlocutorUser = await userAccountService.GetByIdAsync(message.InterlocutorId);
        userMessages = await FileIO.ReadAsync<UserMessage>(Constants.USER_MESSAGE_PATH);
        var existUserMessage = userMessages.FirstOrDefault(x => x.Id == id && !x.IsDeleted)
           ?? throw new Exception($"This message is not found with this id{id}");

        existUserMessage.InterlocutorId = message.InterlocutorId;
        existUserMessage.SenderAccountId = message.UserAccountId;
        existUserMessage.MessageContent = message.MessageContent;
        await FileIO.WriteAsync(Constants.USER_MESSAGE_PATH, userMessages);

        return existUserMessage.ToMapped();
    }
}
