using SocialMediaPlatform.Interfaces;
using SocialMediaPlatform.Models.PostsComments;
using SocialMediaPlatform.Models.UserPosts;
using SocialMediaPlatform.Services;
using Spectre.Console;

namespace SocialMediaPlatform.Display;

public class PostCommentMenu
{
    private readonly PostCommentService postCommentService;
    private readonly UserPostsService userPostsService;
    public PostCommentMenu(PostCommentService postCommentService, UserPostsService userPostsService)
    {
        this.postCommentService = postCommentService;
        this.userPostsService = userPostsService;
    }

    public async ValueTask DisplayAsync()
    {
        bool circle = true;
        var options = new string[] { "Create", "GetById", "Update", "Delete", "GetByPostId", "Back" };
        var title = "-- Post comments --";

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = Selection.SelectionMenu(title, options);
            switch (selection)
            {
                case "Create":
                    await CreateAsync();
                    break;
                case "GetById":
                    await GetByIdAsync();
                    break;
                case "Update":
                    await UpdateAsync();
                    break;
                case "Delete":
                    await DeleteAsync();
                    break;
                case "GetByPostId":
                    await GetByPostId();
                    break;
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
    async ValueTask CreateAsync()
    {
        Console.Clear();
        int id = AnsiConsole.Ask<int>("Enter Post Id : ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            id = AnsiConsole.Ask<int>("Enter Post Id : ");
        }

        int beholderId = AnsiConsole.Ask<int>("Enter userAccount Id : ");
        while (beholderId <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            beholderId = AnsiConsole.Ask<int>("Enter userAccount Id : ");
        }
        string postComment = AnsiConsole.Ask<string>("Enter post comment : ");
        PostsCommentCreationModel comment = new ()
        {
            PostId = id,
            BeholderId = beholderId,
            CommentContent = postComment,
        };
        try
        {
            var addedComment = await postCommentService.CreateAsync(comment);
            AnsiConsole.Markup("[orange3]Succesful created[/]\n");
            var table = Selection.DataTable("Comment", addedComment);
            AnsiConsole.Write(table);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }
    async ValueTask UpdateAsync()
    {
        Console.Clear();
        int id = AnsiConsole.Ask<int>("Enter post Id to update: ");
        int postId = AnsiConsole.Ask<int>("Enter Post Id : ");
        while (postId <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            postId = AnsiConsole.Ask<int>("Enter Post Id : ");
        }

        int beholderId = AnsiConsole.Ask<int>("Enter userAccount Id : ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            id = AnsiConsole.Ask<int>("Enter userAccount Id : ");
        }
        string postComment = AnsiConsole.Ask<string>("Enter post comment : ");
        PostsCommentUpdateModel comment = new()
        {
            PostId = id,
            BeholderId = beholderId,
            CommentContent = postComment,
        };

        try
        {
            var updatedComment = await postCommentService.UpdateAsync(id, comment);
            AnsiConsole.Markup("[orange3]Succesful updated[/]\n");
           
            var table = Selection.DataTable("Comment", updatedComment);
            AnsiConsole.Write(table);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }

    async ValueTask GetByIdAsync()
    {
        Console.Clear();
        int id = AnsiConsole.Ask<int>("Enter comment Id: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            id = AnsiConsole.Ask<int>("Enter comment Id: ");
        }

        try
        {
            var comment = await postCommentService.GetByIdAsync(id);
            
            var table = Selection.DataTable("Comment", comment);
            AnsiConsole.Write(table);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }

    async ValueTask DeleteAsync()
    {
        Console.Clear();
        int id = AnsiConsole.Ask<int>("Enter comment Id to delete: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            id = AnsiConsole.Ask<int>("Enter comment Id to delete: ");
        }

        try
        {
            await postCommentService.DeleteAsync(id);
            AnsiConsole.Markup("[orange3]Succesful deleted[/]\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }

    async ValueTask GetByPostId()
    {
        Console.Clear();
        int id = AnsiConsole.Ask<int>("Enter post Id : ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            id = AnsiConsole.Ask<int>("Enter post Id : ");
        }
        var post = await userPostsService.GetByIdAsync(id);
        var image = new CanvasImage(post.Content);
        image.MaxWidth(16);

        AnsiConsole.Write(image);
        var comments = await postCommentService.GetByPostIdAsync(id);
        var table = Selection.DataTable("Comments", comments.ToList());
        AnsiConsole.Write(table);
        
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }
}
