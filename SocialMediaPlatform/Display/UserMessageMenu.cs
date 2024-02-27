using SocialMediaPlatform.Interfaces;
using SocialMediaPlatform.Models.UserMessages;
using SocialMediaPlatform.Models.Users;
using SocialMediaPlatform.Services;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace SocialMediaPlatform.Display;

public class UserMessageMenu
{
    private UserMessageService userMessageService;
    private UserAccountService userAccountService;
    public UserMessageMenu(UserMessageService userMessageService, UserAccountService userAccountService)
    {
        this.userMessageService = userMessageService;
        this.userAccountService = userAccountService;
    }

    public async ValueTask DisplayAsync()
    {
        bool circle = true;
        var options = new string[] { "Create", "GetById", "Update", "Delete", "GetByUserAccountId", "Back" };
        var title = "-- User Message --";

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
                    await GetByUserAccountId();
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

        int InterlocutorId = AnsiConsole.Ask<int>("Enter InterlocutorId Id : ");
        while (InterlocutorId <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            InterlocutorId = AnsiConsole.Ask<int>("Enter InterlocutorId Id : ");
        }
        string messageContent = AnsiConsole.Ask<string>("Enter message : ");

        UserMessageCreationModel message = new UserMessageCreationModel()
        {
            UserAccountId = id,
            InterlocutorId = InterlocutorId,
            MessageContent = messageContent
        };
        try
        {
            var addedMessage = await userMessageService.CreateAsync(message);
            AnsiConsole.Markup("[orange3]Succesful created[/]\n");

            var table = Selection.DataTable("Message", addedMessage);
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
        int id = AnsiConsole.Ask<int>("Enter message Id to update: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            id = AnsiConsole.Ask<int>("Enter message Id to update: ");
        }
        int UserAccountId = AnsiConsole.Ask<int>("Enter UserAccount Id : ");
        while (UserAccountId <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            UserAccountId = AnsiConsole.Ask<int>("Enter UserAccount Id : ");
        }

        int InterlocutorId = AnsiConsole.Ask<int>("Enter InterlocutorId Id : ");
        while (InterlocutorId <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            InterlocutorId = AnsiConsole.Ask<int>("Enter InterlocutorId Id : ");
        }
        string messageContent = AnsiConsole.Ask<string>("Enter message : ");

        UserMessageUpdateModel message = new UserMessageUpdateModel()
        {
            UserAccountId = UserAccountId,
            InterlocutorId = InterlocutorId,
            MessageContent = messageContent
        };
        try
        {
            var updatedMessage = await userMessageService.UpdateAsync(id, message);
            AnsiConsole.Markup("[orange3]Succesful updated[/]\n");

            var table = Selection.DataTable("Message", updatedMessage);
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
    async ValueTask GetByUserAccountId()
    {
        Console.Clear();
        int UserAccountId = AnsiConsole.Ask<int>("Enter UserAccount Id : ");
        while (UserAccountId <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            UserAccountId = AnsiConsole.Ask<int>("Enter UserAccount Id : ");
        }
        int InterlocutorId = AnsiConsole.Ask<int>("Enter InterlocutorId Id : ");
        while (InterlocutorId <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            InterlocutorId = AnsiConsole.Ask<int>("Enter InterlocutorId Id : ");
        }
        var users = await userMessageService.GetByUserAccountIdAsync(UserAccountId,InterlocutorId);
        var table = Selection.DataTable("Users", users.ToList());
        AnsiConsole.Write(table);
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();
    }

    async ValueTask DeleteAsync()
    {
        Console.Clear();
        int id = AnsiConsole.Ask<int>("Enter message Id to delete: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            id = AnsiConsole.Ask<int>("Enter message Id to delete: ");
        }

        try
        {
            await userMessageService.DeleteAsync(id);
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
        int id = AnsiConsole.Ask<int>("Enter message Id: ");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine("Was entered in the wrong format .Try again!");
            id = AnsiConsole.Ask<int>("Enter message Id: ");
        }

        try
        {
            var message = await userMessageService.GetByIdAsync(id);
            var table = Selection.DataTable("Message", message);
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
