using SocialMediaPlatform.Interfaces;
using SocialMediaPlatform.Models.PostsComments;
using SocialMediaPlatform.Models.PostsLikes;
using SocialMediaPlatform.Services;
using Spectre.Console;

namespace SocialMediaPlatform.Display;

public class PostLikeMenu
{
    private readonly PostLikesService postLikesService;
    private readonly UserPostsService userPostsService;
    public PostLikeMenu(PostLikesService postLikesService,UserPostsService userPostsService)
    {
        this.postLikesService = postLikesService;
        this.userPostsService = userPostsService;
    }

    public async ValueTask DisplayAsync()
    {
        bool circle = true;
        var options = new string[] { "Create", "GetById", "Delete", "GetByPostId", "Back" };
        var title = "-- Post likes --";

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
        PostsLikeCreationModel like = new()
        {
            PostId = id,
            BeholderId = beholderId,
        };
        try
        {
            var addedLike = await postLikesService.CreateAsync(like);
            AnsiConsole.Markup("[orange3]Succesful created[/]\n");
            var table = Selection.DataTable("Like", addedLike);
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
        int id = AnsiConsole.Ask<int>("Enter like Id: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            id = AnsiConsole.Ask<int>("Enter like Id: ");
        }

        try
        {
            var like = await postLikesService.GetByIdAsync(id);

            var table = Selection.DataTable("Like", like);
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
        int id = AnsiConsole.Ask<int>("Enter like Id to delete: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            id = AnsiConsole.Ask<int>("Enter like Id to delete: ");
        }

        try
        {
            await postLikesService.DeleteAsync(id);
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
        var likes = await postLikesService.GetByPostIdAsync(id);
        var table = Selection.DataTable("Likes", likes.ToList());
        AnsiConsole.Write(table);

        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }
}
