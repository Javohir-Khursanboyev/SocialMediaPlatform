using SocialMediaPlatform.Models.PostsComments;
using SocialMediaPlatform.Models.PostsLikes;
using SocialMediaPlatform.Models.UserAccounts;
using SocialMediaPlatform.Models.UserMessages;
using SocialMediaPlatform.Models.UserPosts;
using SocialMediaPlatform.Models.Users;
using Spectre.Console;
using System.Xml.Linq;

namespace SocialMediaPlatform.Display;

public static partial class Selection
{
    public static Table DataTable(string title, UserViewModel user)
    {
        var table = new Table();
        table.Title(title);
        table.AddColumn("[slateblue1]Id[/]");
        table.AddColumn("[slateblue1]FirstName[/]");
        table.AddColumn("[slateblue1]LastName[/]");
        table.AddColumn("[slateblue1]DateOfBirth[/]");
        table.AddColumn("[slateblue1]Email[/]");

        table.AddRow(user.Id.ToString(), user.FirstName, user.LastName,user.DateOfBirth.ToString(), user.Email);
        return table;
    }

    public static Table DataTable(string title, List<UserViewModel> users)
    {
        var table = new Table();
        table.Title(title);
        table.AddColumn("[slateblue1]Id[/]");
        table.AddColumn("[slateblue1]FirstName[/]");
        table.AddColumn("[slateblue1]LastName[/]");
        table.AddColumn("[slateblue1]DateOfBirth[/]");
        table.AddColumn("[slateblue1]Email[/]");

        foreach (var user in users)
        {
            table.AddRow(user.Id.ToString(), user.FirstName, user.LastName, user.DateOfBirth.ToString(), user.Email);
        }
        return table;
    }
}

public static partial class Selection
{
    public static Table DataTable(string title, UserAccountViewModel user)
    {
        var table = new Table();
        table.Title(title);
        table.AddColumn("[slateblue1]Id[/]");
        table.AddColumn("[slateblue1]UserId[/]");
        table.AddColumn("[slateblue1]NickName[/]");
        table.AddColumn("[slateblue1]Bio[/]");
        table.AddColumn("[slateblue1]Privacy[/]");

        table.AddRow(user.Id.ToString(), user.UserId.ToString(), user.NickName, user.Bio, user.Privacy.ToString());
        return table;
    }

    public static Table DataTable(string title, List<UserAccountViewModel> users)
    {
        var table = new Table();
        table.Title(title);
        table.AddColumn("[slateblue1]Id[/]");
        table.AddColumn("[slateblue1]UserId[/]");
        table.AddColumn("[slateblue1]NickName[/]");
        table.AddColumn("[slateblue1]Bio[/]");
        table.AddColumn("[slateblue1]Privacy[/]");

        foreach (var user in users)
        {
            table.AddRow(user.Id.ToString(), user.UserId.ToString(), user.NickName, user.Bio, user.Privacy.ToString());
        }
        return table;
    }
}

public static partial class Selection
{
    public static Table DataTable(string title, UserPostViewModel post)
    {
        var table = new Table();
        table.Title(title);
        table.AddColumn("[slateblue1]Id[/]");
        table.AddColumn("[slateblue1]UserAccountId[/]");
        table.AddColumn("[slateblue1]Comment[/]");
        table.AddColumn("[slateblue1]LikeCount[/]");

        table.AddRow(post.Id.ToString(), post.UserAccountId.ToString(), post.Comment,post.LikeCount.ToString());
        return table;
    }
}

public static partial class Selection
{
    public static Table DataTable(string title, PostsCommentViewModel comment)
    {
        var table = new Table();
        table.Title(title);
        table.AddColumn("[slateblue1]Id[/]");
        table.AddColumn("[slateblue1]PostId[/]");
        table.AddColumn("[slateblue1]BeholderId[/]");
        table.AddColumn("[slateblue1]Comment[/]");
        table.AddColumn("[slateblue1]Time[/]");

        table.AddRow(comment.Id.ToString(), comment.PostId.ToString(),comment.BeholderId.ToString(), comment.CommentContent, comment.Time.ToString());
        return table;
    }

    public static Table DataTable(string title, List<PostsCommentViewModel> comments)
    {
        var table = new Table();
        table.Title(title);
        table.AddColumn("[slateblue1]Id[/]");
        table.AddColumn("[slateblue1]PostId[/]");
        table.AddColumn("[slateblue1]BeholderId[/]");
        table.AddColumn("[slateblue1]Comment[/]");
        table.AddColumn("[slateblue1]Time[/]");

        foreach (var comment in comments)
        {
            table.AddRow(comment.Id.ToString(), comment.PostId.ToString(), comment.BeholderId.ToString(), comment.CommentContent, comment.Time.ToString());
        }
        return table;
    }
}

public static partial class Selection
{
    public static Table DataTable(string title, PostLikeViewModel like)
    {
        var table = new Table();
        table.Title(title);
        table.AddColumn("[slateblue1]Id[/]");
        table.AddColumn("[slateblue1]PostId[/]");
        table.AddColumn("[slateblue1]BeholderId[/]");
        table.AddColumn("[slateblue1]Time[/]");

        table.AddRow(like.Id.ToString(), like.PostId.ToString(), like.BeholderId.ToString(), like.Time.ToString());
        return table;
    }

    public static Table DataTable(string title, List<PostLikeViewModel> likes)
    {
        var table = new Table();
        table.Title(title);
        table.AddColumn("[slateblue1]Id[/]");
        table.AddColumn("[slateblue1]PostId[/]");
        table.AddColumn("[slateblue1]BeholderId[/]");
        table.AddColumn("[slateblue1]Time[/]");

        foreach (var like in likes)
        {
            table.AddRow(like.Id.ToString(), like.PostId.ToString(), like.BeholderId.ToString(), like.Time.ToString());
        }
        return table;
    }
}

public static partial class Selection
{
    public static Table DataTable(string title, UserMessageViewModel message)
    {
        var table = new Table();
        table.Title(title);
        table.AddColumn("[slateblue1]Id[/]");
        table.AddColumn("[slateblue1]SenderAccountId[/]");
        table.AddColumn("[slateblue1]InterlocutorId[/]");
        table.AddColumn("[slateblue1]Message[/]");
        table.AddColumn("[slateblue1]Time[/]");

        table.AddRow(message.Id.ToString(), message.UserAccountId.ToString(), message.InterlocutorId.ToString(),message.MessageContent, message.Time.ToString());
        return table;
    }

    public static Table DataTable(string title, List<UserMessageViewModel> messages)
    {
        var table = new Table();
        table.Title(title);
        table.AddColumn("[slateblue1]Id[/]");
        table.AddColumn("[slateblue1]SenderAccountId[/]");
        table.AddColumn("[slateblue1]InterlocutorId[/]");
        table.AddColumn("[slateblue1]Message[/]");
        table.AddColumn("[slateblue1]Time[/]");

        foreach (var message in messages)
        {
            table.AddRow(message.Id.ToString(), message.UserAccountId.ToString(), message.InterlocutorId.ToString(), message.MessageContent, message.Time.ToString());
        }
        return table;
    }
}
public static partial class Selection
{
    public static string SelectionMenu(string title, string[] options)
    {
        var selection = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .Title($"[darkorange3_1]{title}[/]")
        .PageSize(5)
        .AddChoices(options)
        .HighlightStyle(new Style(foreground: Color.White, background: Color.Blue))
        );
        return selection;
    }
    public static Table DataTable()
    {
        var table = new Table();

        table.AddColumn("[blue]1.User[/]");
        table.AddColumn("[blue]2.UserAccount[/]");
        table.AddColumn("[blue]3.UserMessage[/]");
        table.AddColumn("[blue]4.User Posts[/]");
        table.AddColumn("[blue]5.Posts Comments[/]");
        table.AddColumn("[blue]6.Posts Likes[/]");
        table.AddColumn("[blue]7.Saved post[/]");
        table.AddColumn("[red]8.Exit[/]");

        return table;
    }
}
