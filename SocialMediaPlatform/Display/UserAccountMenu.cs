using SocialMediaPlatform.Models.UserAccounts;
using SocialMediaPlatform.Services;
using Spectre.Console;

namespace SocialMediaPlatform.Display;

public class UserAccountMenu
{
    private readonly UserAccountService userAccountService;
    public UserAccountMenu(UserAccountService userAccountService)
    {
        this.userAccountService = userAccountService;
    }
    public async ValueTask DisplayAsync()
    {
        bool circle = true;
        var options = new string[] { "Create", "GetById", "Update", "Delete", "GetAll", "Back" };
        var title = "-- UserAccount --";

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
        int userId = AnsiConsole.Ask<int>("Enter user Id : ");
        while (userId <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            userId = AnsiConsole.Ask<int>("Enter user Id : ");
        }
        string nickName = AnsiConsole.Ask<string>("Enter nickName : ");
        string bio = AnsiConsole.Ask<string>("Enter Bio : ");

        UserAccountCreationModel user = new()
        {
            UserId = userId,
            NickName = nickName,
            Bio = bio,
        };
        Console.WriteLine("1.Public\n2.Privacy");
        int choice = AnsiConsole.Ask<int>("choice : ");
        while (choice <= 0 || choice > 3)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            choice = AnsiConsole.Ask<int>(" choice : ");
        }
        switch (choice)
        {
            case 1: user.Privacy = Enums.Privacy.Public; break;
            case 2: user.Privacy = Enums.Privacy.Private; break;
        }
        try
        {
            var addedUser = await userAccountService.CreateAsync(user);
            AnsiConsole.Markup("[orange3]Succesful created[/]\n");

            var table = Selection.DataTable("UserAccount", addedUser);
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
        int id = AnsiConsole.Ask<int>("Enter UserAccount Id to update: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            id = AnsiConsole.Ask<int>("Enter UserAccount Id to update: ");
        }
        int userId = AnsiConsole.Ask<int>("Enter user Id : ");
        while (userId <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            userId = AnsiConsole.Ask<int>("Enter user Id : ");
        }
        string nickName = AnsiConsole.Ask<string>("Enter nickName : ");
        string bio = AnsiConsole.Ask<string>("Enter Bio : ");

        UserAccountUpdateModel user = new()
        {
            UserId = userId,
            NickName = nickName,
            Bio = bio,
        };
        Console.WriteLine("1.Public\n2.Privacy");
        int choice = AnsiConsole.Ask<int>("choice : ");
        while (choice <= 0 || choice > 3)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            choice = AnsiConsole.Ask<int>(" choice : ");
        }
        switch (choice)
        {
            case 1: user.Privacy = Enums.Privacy.Public; break;
            case 2: user.Privacy = Enums.Privacy.Private; break;
        }
        try
        {
            var updatedUser = await userAccountService.UpdateAsync(id, user);
            AnsiConsole.Markup("[orange3]Succesful updated[/]\n");

            var table = Selection.DataTable("UserAccount", updatedUser);
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
    async ValueTask GetAllAsync()
    {
        Console.Clear();
        var users = await userAccountService.GetAllAsync();
        var table = Selection.DataTable("UserAccounts", users.ToList());
        AnsiConsole.Write(table);
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }

    async ValueTask DeleteAsync()
    {
        Console.Clear();
        int id = AnsiConsole.Ask<int>("Enter UserAccount Id to delete: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            id = AnsiConsole.Ask<int>("Enter UserAccount Id to delete: ");
        }

        try
        {
            await userAccountService.DeleteAsync(id);
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

    async ValueTask GetByIdAsync()
    {
        Console.Clear();
        long id = AnsiConsole.Ask<long>("Enter UserAccounts Id: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            id = AnsiConsole.Ask<int>("Enter UserAccounts Id: ");
        }

        try
        {
            var user = await userAccountService.GetByIdAsync(id);
            var table = Selection.DataTable("userProfile", user);
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
}
