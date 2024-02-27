using SocialMediaPlatform.Models.UserPosts;
using SocialMediaPlatform.Services;
using Spectre.Console;

namespace SocialMediaPlatform.Display;

public class UserPostsMenu
{
    private UserPostsService userPostsService;
    public UserPostsMenu(UserPostsService userPostsService)
    {
        this.userPostsService = userPostsService;
    }

    public async ValueTask DisplayAsync()
    {
        bool circle = true;
        var options = new string[] { "Create", "GetById", "Update", "Delete", "GetByUserAccountId", "GetAll", "Back" };
        var title = "-- Posts --";

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
                case "GetByUserAccountId":
                    await GetByUserAccountIdAsync();
                    break;
                case "GetAll":
                    await GetAllAsync();
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
        int id = AnsiConsole.Ask<int>("Enter UserAccount Id : ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            id = AnsiConsole.Ask<int>("Enter UserAccount Id : ");
        }
        string content = AnsiConsole.Ask<string>("Enter content path (.png): ");
        while(Path.GetExtension(content).ToLower()!=".png")
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            content = AnsiConsole.Ask<string>("Enter content path (.png): ");
        }
        string postComment = AnsiConsole.Ask<string>("Enter post comment : ");
        UserPostCreationModel post = new UserPostCreationModel()
        {
            UserAccountId = id,
            Comment = postComment,
            Content = content
        };
        try
        {
            var addedPost = await userPostsService.CreateAsync(post);
            AnsiConsole.Markup("[orange3]Succesful created[/]\n");
            var image = new CanvasImage(content);
            image.MaxWidth(16);

            AnsiConsole.Write(image);
            var table = Selection.DataTable("Post", addedPost);
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
        int userAccountId = AnsiConsole.Ask<int>("Enter UserAccount Id : ");
        while (userAccountId <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            userAccountId = AnsiConsole.Ask<int>("Enter UserAccount Id : ");
        }
        string content = AnsiConsole.Ask<string>("Enter content path (.png): ");
        while (Path.GetExtension(content).ToLower() != ".png")
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            content = AnsiConsole.Ask<string>("Enter content path (.png): ");
        }
        string postComment = AnsiConsole.Ask<string>("Enter post comment : ");
        UserPostUpdateModel post = new()
        {
            UserAccountId = id,
            Comment = postComment,
            Content = content
        };
        try
        {
            var updatedPost = await userPostsService.UpdateAsync(id, post);
            AnsiConsole.Markup("[orange3]Succesful updated[/]\n");
            var image = new CanvasImage(content);
            image.MaxWidth(16);

            AnsiConsole.Write(image);
            var table = Selection.DataTable("Post", updatedPost);
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
        int id = AnsiConsole.Ask<int>("Enter post Id: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            id = AnsiConsole.Ask<int>("Enter post Id: ");
        }

        try
        {
            var post = await userPostsService.GetByIdAsync(id);
            var image = new CanvasImage(post.Content);
            image.MaxWidth(16);

            AnsiConsole.Write(image);
            var table = Selection.DataTable("Post", post);
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
        int id = AnsiConsole.Ask<int>("Enter post Id to delete: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            id = AnsiConsole.Ask<int>("Enter post Id to delete: ");
        }

        try
        {
            await userPostsService.DeleteAsync(id);
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

    async ValueTask GetAllAsync()
    {
        Console.Clear();
        var posts = await userPostsService.GetAllAsync();
        foreach(var post in posts)
        {
            var image = new CanvasImage(post.Content);
            image.MaxWidth(16);

            AnsiConsole.Write(image);
            var table = Selection.DataTable("Post", post);
            AnsiConsole.Write(table);
        }
        
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }

    async ValueTask GetByUserAccountIdAsync()
    {
        Console.Clear();
        int id = AnsiConsole.Ask<int>("Enter UserAccount Id : ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            id = AnsiConsole.Ask<int>("Enter UserAccount Id : ");
        }
        var posts = await userPostsService.GetByUserAccountIdAsync(id);
        foreach (var post in posts)
        {
            var image = new CanvasImage(post.Content);
            image.MaxWidth(16);

            AnsiConsole.Write(image);
            var table = Selection.DataTable("Post", post);
            AnsiConsole.Write(table);
        }

        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }
}
