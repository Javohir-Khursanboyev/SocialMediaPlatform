using SocialMediaPlatform.Services;
using Spectre.Console;

namespace SocialMediaPlatform.Display;

public class SavedPostMenu
{
    private readonly SavedPostsService savedPostsService;
    public SavedPostMenu(SavedPostsService savedPostsService)
    {
        this.savedPostsService = savedPostsService;
    }

    public async ValueTask DisplayAsync()
    {
        bool circle = true;
        var options = new string[] { "Create", "GetById", "Delete", "GetByUserAccountId", "Back" };
        var title = "-- Saved post --";

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = Selection.SelectionMenu(title, options);
            switch (selection)
            {
                //case "Create":
                //    await CreateAsync();
                //    break;
                //case "GetById":
                //    await GetByIdAsync();
                //    break;
                //case "Delete":
                //    await DeleteAsync();
                //    break;
                //case "GetByPostId":
                //    await GetByUserAccountId();
                //    break;
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
