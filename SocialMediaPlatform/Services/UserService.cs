using SocialMediaPlatform.Configurations;
using SocialMediaPlatform.Extensions;
using SocialMediaPlatform.Helpers;
using SocialMediaPlatform.Interfaces;
using SocialMediaPlatform.Models.Users;

namespace SocialMediaPlatform.Services;

public class UserService : IUserService
{
    private List<User> users;
    public async ValueTask<UserViewModel> CreateAsync(UserCreationModel user)
    {
        users = await FileIO.ReadAsync<User>(Constants.USER_PATH);
        var existUser = users.FirstOrDefault(u => u.Email == user.Email);
        if (existUser is not null)
        {
            if (existUser.IsDeleted)
                return await UpdateAsync(existUser.Id, user.ToMap(), true);
            throw new Exception($"This user is already exist with this email :{user.Email}");
        }
        var createdUser = users.Create(user.ToMapped());
        await FileIO.WriteAsync(Constants.USER_PATH, users);
        return createdUser.ToMapped();
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        users = await FileIO.ReadAsync<User>(Constants.USER_PATH);
        var existUser = users.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
            ?? throw new Exception($"This user is not found with this id {id}");

        existUser.IsDeleted = true;
        existUser.DeletedAt = DateTime.UtcNow;
        await FileIO.WriteAsync(Constants.USER_PATH, users);

        return true;
    }

    public async ValueTask<IEnumerable<UserViewModel>> GetAllAsync()
    {
        users = await FileIO.ReadAsync<User>(Constants.USER_PATH);
        return users.Where(u => !u.IsDeleted).ToList().ToMapped();
    }

    public async ValueTask<UserViewModel> GetByIdAsync(long id)
    {
        users = await FileIO.ReadAsync<User>(Constants.USER_PATH);
        var existUser = users.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
            ?? throw new Exception($"This user is not found with this id {id}");

        return existUser.ToMapped();
    }

    public async ValueTask<UserViewModel> UpdateAsync(long id, UserUpdateModel user, bool isUsesDeleted = false)
    {
        users = await FileIO.ReadAsync<User>(Constants.USER_PATH);
        var existUser = new User();
        if (isUsesDeleted)
            existUser = users.FirstOrDefault(u => u.Id == id);
        else
            existUser = users.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
                ?? throw new Exception($"This user is not found with this Id {id}");

        existUser.IsDeleted = false;
        existUser.Email = user.Email;
        existUser.Password = user.Password;
        existUser.LastName = user.LastName;
        existUser.FirstName = user.FirstName;
        existUser.UpdatedAt = DateTime.UtcNow;
        existUser.DateOfBirth = user.DateOfBirth;
        await FileIO.WriteAsync(Constants.USER_PATH, users);

        return existUser.ToMapped();
    }
}
