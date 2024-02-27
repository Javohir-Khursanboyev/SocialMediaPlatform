using SocialMediaPlatform.Models.Users;
using SocialMediaPlatform.Services;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace SocialMediaPlatform.Display;

public class UserMenu
{
    private readonly UserService userService;
    public UserMenu(UserService userService)
    {
        this.userService = userService;
    }
    public async ValueTask DisplayAsync()
    {
        bool circle = true;
        var options = new string[] { "Create", "GetById", "Update", "Delete", "GetAll", "Back" };
        var title = "-- UserMenu --";

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
        string firstName = AnsiConsole.Ask<string>("FirstName:");
        string lastName = AnsiConsole.Ask<string>("LastName:");
        DateTime dateOfBirth = AnsiConsole.Ask<DateTime>("Enter dateOfBirth mm.dd.year:");
        Console.Write("Enter new Email (email@gmail.com):");
        string email = Console.ReadLine();
        while (!Regex.IsMatch(email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]{2,}$"))
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter new Email (email@gmail.com):");
            email = Console.ReadLine();
        }

        string password = AnsiConsole.Prompt(
        new TextPrompt<string>("Enter password :")
         .PromptStyle("red")
         .Secret());

        UserCreationModel user = new()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password,
            DateOfBirth = dateOfBirth
        };
        try
        {
            var addedUser = await userService.CreateAsync(user);
            AnsiConsole.Markup("[orange3]Succesful created[/]\n");

            var table = Selection.DataTable("User", addedUser);
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
        int id = AnsiConsole.Ask<int>("Enter user Id to update: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            id = AnsiConsole.Ask<int>("Enter user Id to update: ");
        }
        string firstName = AnsiConsole.Ask<string>("FirstName:");
        string lastName = AnsiConsole.Ask<string>("LastName:");
        DateTime dateOfBirth = AnsiConsole.Ask<DateTime>("Enter dateOfBirth mm.dd.year:");
        Console.Write("Enter new Email (email@gmail.com):");
        string email = Console.ReadLine();
        while (!Regex.IsMatch(email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]{2,}$"))
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter new Email (email@gmail.com):");
            email = Console.ReadLine();
        }

        string password = AnsiConsole.Prompt(
        new TextPrompt<string>("Enter password :")
         .PromptStyle("red")
         .Secret());

        UserUpdateModel user = new()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        try
        {
            var updatedUser = await userService.UpdateAsync(id, user);
            AnsiConsole.Markup("[orange3]Succesful updated[/]\n");

            var table = Selection.DataTable("User", updatedUser);
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
        var users = await userService.GetAllAsync();
        var table = Selection.DataTable("Users", users.ToList());
        AnsiConsole.Write(table);
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }

    async ValueTask DeleteAsync()
    {
        Console.Clear();
        int id = AnsiConsole.Ask<int>("Enter user Id to delete: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            id = AnsiConsole.Ask<int>("Enter user Id to delete: ");
        }

        try
        {
            await userService.DeleteAsync(id);
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
        int id = AnsiConsole.Ask<int>("Enter user Id: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            id = AnsiConsole.Ask<int>("Enter user Id: ");
        }

        try
        {
            var user = await userService.GetByIdAsync(id);
            var table = Selection.DataTable("User", user);
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
