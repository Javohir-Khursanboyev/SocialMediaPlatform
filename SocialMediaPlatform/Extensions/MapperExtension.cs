using SocialMediaPlatform.Models.PostsComments;
using SocialMediaPlatform.Models.PostsLikes;
using SocialMediaPlatform.Models.SavedPosts;
using SocialMediaPlatform.Models.UserAccounts;
using SocialMediaPlatform.Models.UserMessages;
using SocialMediaPlatform.Models.UserPosts;
using SocialMediaPlatform.Models.Users;
using System.Reflection;

namespace SocialMediaPlatform.Extensions;

public static partial class MapperExtension
{
    public static User ToMapped(this UserCreationModel model)
    {
        return new User()
        {
            Email = model.Email,
            LastName = model.LastName,
            Password = model.Password,
            FirstName = model.FirstName,
            DateOfBirth = model.DateOfBirth
        };
    }

    public static User ToMapped(this UserUpdateModel model)
    {
        return new User()
        {
            Email = model.Email,
            LastName = model.LastName,
            Password = model.Password,
            FirstName = model.FirstName,
            DateOfBirth = model.DateOfBirth
        };
    }

    public static UserViewModel ToMapped(this User model)
    {
        return new UserViewModel()
        {
            Id = model.Id,
            Email = model.Email,
            LastName = model.LastName,
            FirstName = model.FirstName,
            DateOfBirth = model.DateOfBirth
        };
    }

    public static List<UserViewModel> ToMapped(this List<User> models)
    {
        return models.Select(model => new UserViewModel()
        {
            Id = model.Id,
            Email = model.Email,
            LastName = model.LastName,
            FirstName = model.FirstName,
            DateOfBirth = model.DateOfBirth
        }).ToList();
    }

    public static UserUpdateModel ToMap(this UserCreationModel model)
    {
        return new UserUpdateModel()
        {
            Email = model.Email,
            LastName = model.LastName,
            Password = model.Password,
            FirstName = model.FirstName,
            DateOfBirth = model.DateOfBirth
        };
    }
}
public static partial class MapperExtension
{
    public static UserAccount ToMapped(this UserAccountCreationModel model)
    {
        return new UserAccount()
        {
           Bio = model.Bio,
           UserId = model.UserId,
           Privacy = model.Privacy,
           NickName = model.NickName
        };
    }

    public static UserAccount ToMapped(this UserAccountUpdateModel model)
    {
        return new UserAccount()
        {
            Bio = model.Bio,
            UserId = model.UserId,
            Privacy = model.Privacy,
            NickName = model.NickName
        };
    }

    public static UserAccountViewModel ToMapped(this UserAccount model)
    {
        return new UserAccountViewModel()
        {
            Id = model.Id,
            Bio = model.Bio,
            UserId = model.UserId,
            Privacy = model.Privacy,
            NickName = model.NickName
        };
    }

    public static List<UserAccountViewModel> ToMapped(this List<UserAccount> models)
    {
        return models.Select(model => new UserAccountViewModel()
        {
            Id = model.Id,
            Bio = model.Bio,
            UserId = model.UserId,
            Privacy = model.Privacy,
            NickName = model.NickName
        }).ToList();
    }

    public static UserAccountUpdateModel ToMap(this UserAccountCreationModel model)
    {
        return new UserAccountUpdateModel()
        {
            Bio = model.Bio,
            UserId = model.UserId,
            Privacy = model.Privacy,
            NickName = model.NickName
        };
    }
}
public static partial class MapperExtension
{
    public static UserPost ToMapped(this UserPostCreationModel model)
    {
        return new UserPost()
        {
            Content = model.Content,
            Comment = model.Comment,
            UserAccountId = model.UserAccountId
        };
    }

    public static UserPost ToMapped(this UserPostUpdateModel model)
    {
        return new UserPost()
        {
            Time = model.Time,
            Content = model.Content,
            Comment = model.Comment,
            LikeCount = model.LikeCount,
            UserAccountId = model.UserAccountId
        };
    }

    public static UserPostViewModel ToMapped(this UserPost model)
    {
        return new UserPostViewModel()
        {
            Id = model.Id,
            Content = model.Content,
            Comment = model.Comment,
            LikeCount = model.LikeCount,
            UserAccountId = model.UserAccountId
        };
    }

    public static List<UserPostViewModel> ToMapped(this List<UserPost> models)
    {
        return models.Select(model => new UserPostViewModel()
        {
            Id = model.Id,
            Content = model.Content,
            Comment = model.Comment,
            LikeCount = model.LikeCount,
            UserAccountId = model.UserAccountId
        }).ToList();
    }

    public static UserPostUpdateModel ToMap(this UserPostCreationModel model)
    {
        return new UserPostUpdateModel()
        {
            Content = model.Content,
            Comment = model.Comment,
            UserAccountId = model.UserAccountId
        };
    }
    public static UserPostUpdateModel ToMap(this UserPostViewModel model)
    {
        return new UserPostUpdateModel()
        {
            Content = model.Content,
            Comment = model.Comment,
            LikeCount = model.LikeCount,
            UserAccountId = model.UserAccountId
        };
    }
}
public static partial class MapperExtension
{
    public static PostsComment ToMapped(this PostsCommentCreationModel model)
    {
        return new PostsComment()
        {
            PostId = model.PostId,
            BeholderId = model.BeholderId,
            CommentContent = model.CommentContent
        };
    }

    public static PostsComment ToMapped(this PostsCommentUpdateModel model)
    {
        return new PostsComment()
        {
            PostId = model.PostId,
            BeholderId = model.BeholderId,
            CommentContent = model.CommentContent
        };
    }

    public static PostsCommentViewModel ToMapped(this PostsComment model)
    {
        return new PostsCommentViewModel()
        {
            Id = model.Id,
            Time = model.Time,
            PostId = model.PostId,
            BeholderId = model.BeholderId,
            CommentContent = model.CommentContent
        };
    }

    public static List<PostsCommentViewModel> ToMapped(this List<PostsComment> models)
    {
        return models.Select(model => new PostsCommentViewModel()
        {
            Id = model.Id,
            Time = model.Time,
            PostId = model.PostId,
            BeholderId = model.BeholderId,
            CommentContent = model.CommentContent
        }).ToList();
    }
}
public static partial class MapperExtension
{
    public static PostsLike ToMapped(this PostsLikeCreationModel model)
    {
        return new PostsLike()
        {
            Time = model.Time,
            PostId = model.PostId,
            BeholderId = model.BeholderId
        };
    }

    public static PostLikeViewModel ToMapped(this PostsLike model)
    {
        return new PostLikeViewModel()
        {
            Id = model.Id,
            Time = model.Time,
            PostId = model.PostId,
            BeholderId = model.BeholderId
        };
    }

    public static List<PostLikeViewModel> ToMapped(this List<PostsLike> models)
    {
        return models.Select(model => new PostLikeViewModel()
        {
            Id = model.Id,
            Time = model.Time,
            PostId = model.PostId,
            BeholderId = model.BeholderId
        }).ToList();
    }
}
public static partial class MapperExtension
{
    public static SavedPost ToMapped(this SavedPostCreationModel model)
    {
        return new SavedPost()
        {
            UserAccountId = model.UserAccountId,
            PostId = model.PostId
        };
    }

    public static SavedPostViewModel ToMapped(this SavedPost model)
    {
        return new SavedPostViewModel()
        {
            Id = model.Id,
            UserAccountId = model.UserAccountId,
            PostId = model.PostId
        };
    }

    public static List<SavedPostViewModel> ToMapped(this List<SavedPost> models)
    {
        return models.Select(model => new SavedPostViewModel()
        {
            Id = model.Id,
            UserAccountId = model.UserAccountId,
            PostId = model.PostId
        }).ToList();
    }
}

public static partial class MapperExtension
{
    public static UserMessage ToMapped(this UserMessageCreationModel model)
    {
        return new UserMessage()
        {
           SenderAccountId = model.UserAccountId,
           InterlocutorId = model.InterlocutorId,
           MessageContent = model.MessageContent
        };
    }

    public static UserMessage ToMapped(this UserMessageUpdateModel model)
    {
        return new UserMessage()
        {
            SenderAccountId = model.UserAccountId,
            InterlocutorId = model.InterlocutorId,
            MessageContent = model.MessageContent
        };
    }

    public static UserMessageViewModel ToMapped(this UserMessage model)
    {
        return new UserMessageViewModel()
        {
            Id = model.Id,
            UserAccountId = model.SenderAccountId,
            InterlocutorId = model.InterlocutorId,
            MessageContent = model.MessageContent,
            Time = model.Time
        };
    }

    public static List<UserMessageViewModel> ToMapped(this List<UserMessage> models)
    {
        return models.Select(model => new UserMessageViewModel()
        {
            Id = model.Id,
            UserAccountId = model.SenderAccountId,
            InterlocutorId = model.InterlocutorId,
            MessageContent = model.MessageContent,
            Time = model.Time
        }).ToList();
    }
}