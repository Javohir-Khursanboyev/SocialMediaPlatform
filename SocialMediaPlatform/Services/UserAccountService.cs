using SocialMediaPlatform.Configurations;
using SocialMediaPlatform.Extensions;
using SocialMediaPlatform.Helpers;
using SocialMediaPlatform.Interfaces;
using SocialMediaPlatform.Models.UserAccounts;

namespace SocialMediaPlatform.Services;

public class UserAccountService : IUserAccountService
{
    private List<UserAccount> userAccounts;
    private UserService userService;
    public UserAccountService(UserService userService)
    {
        this.userService = userService;
    }
    public async ValueTask<UserAccountViewModel> CreateAsync(UserAccountCreationModel user)
    {
        var existUser = await userService.GetByIdAsync(user.UserId);
        userAccounts = await FileIO.ReadAsync<UserAccount>(Constants.USER_ACCOUNT_PATH);

        var existUserAccount = userAccounts.FirstOrDefault(ua => ua.UserId == user.UserId);
        if (existUserAccount is not null)
        {
            if (existUserAccount.IsDeleted)
                return await UpdateAsync(existUserAccount.Id, user.ToMap(), true);
            throw new Exception($"This user is already exist with this UserId {existUserAccount.UserId}");
        }

        existUserAccount = userAccounts.FirstOrDefault(ua => ua.NickName.ToLower() == user.NickName.ToLower());
        if (existUserAccount is not null)
            throw new Exception($"This userAccount is already exist with this NickName {existUserAccount.NickName}");

        var createdUserAccount = userAccounts.Create(user.ToMapped());
        await FileIO.WriteAsync(Constants.USER_ACCOUNT_PATH, userAccounts);
        return createdUserAccount.ToMapped();
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        userAccounts = await FileIO.ReadAsync<UserAccount>(Constants.USER_ACCOUNT_PATH);
        var existUserAccount = userAccounts.FirstOrDefault(ua => ua.Id == id && !ua.IsDeleted)
            ?? throw new Exception($"This user account is not found With this Id {id}");

        existUserAccount.IsDeleted = true;
        existUserAccount.DeletedAt = DateTime.UtcNow;
        await FileIO.WriteAsync(Constants.USER_ACCOUNT_PATH, userAccounts);
        return true;
    }

    public async ValueTask<IEnumerable<UserAccountViewModel>> GetAllAsync()
    {
        userAccounts = await FileIO.ReadAsync<UserAccount>(Constants.USER_ACCOUNT_PATH);
        return userAccounts.Where(ua=>!ua.IsDeleted).ToList().ToMapped();
    }

    public async ValueTask<UserAccountViewModel> GetByIdAsync(long id)
    {
        userAccounts = await FileIO.ReadAsync<UserAccount>(Constants.USER_ACCOUNT_PATH);
        var existUserAccount = userAccounts.FirstOrDefault(ua => ua.Id == id && !ua.IsDeleted)
            ?? throw new Exception($"This user account is not found With this Id {id}");

        return existUserAccount.ToMapped();
    }

    public async ValueTask<UserAccountViewModel> UpdateAsync(long id, UserAccountUpdateModel user, bool isUsesDeleted = false)
    {
        var existUser = await userService.GetByIdAsync(user.UserId);
        userAccounts = await FileIO.ReadAsync<UserAccount>(Constants.USER_ACCOUNT_PATH);
        var existUserAccount = new UserAccount();
        if(isUsesDeleted)
            existUserAccount = userAccounts.FirstOrDefault(ua=>ua.Id == id);
        else
            existUserAccount = userAccounts.FirstOrDefault(ua => ua.Id == id && !ua.IsDeleted)
                ?? throw new Exception($"This user account is not found With this Id {id}");

        existUserAccount.Bio = user.Bio;
        existUserAccount.IsDeleted = false;
        existUserAccount.UserId = user.UserId;
        existUserAccount.Privacy = user.Privacy;
        existUserAccount.NickName = user.NickName;
        existUserAccount.UpdatedAt = DateTime.UtcNow;

        return existUserAccount.ToMapped() ;
    }
}
