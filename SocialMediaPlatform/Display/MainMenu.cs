using SocialMediaPlatform.Services;
using Spectre.Console;

namespace SocialMediaPlatform.Display;

public class MainMenu
{
    private readonly UserMenu userMenu;
    private readonly UserAccountMenu userAccountMenu;
    private readonly UserPostsMenu userPostsMenu;
    private readonly PostCommentMenu postCommentMenu;
    private readonly PostLikeMenu postLikeMenu;
    private readonly SavedPostMenu savedPostMenu;
    private readonly UserMessageMenu userMessageMenu;

    private readonly UserService userService;
    private readonly UserAccountService userAccountService;
    private readonly UserPostsService userPostsService;
    private readonly PostCommentService postCommentService;
    private readonly PostLikesService postLikesService;
    private readonly SavedPostsService savedPostsService;
    private readonly UserMessageService userMessageService;

    public MainMenu()
    {
        userService = new UserService();
        userAccountService = new UserAccountService(userService);
        userPostsService = new UserPostsService(userAccountService);
        postCommentService = new PostCommentService(userPostsService,userAccountService);
        postLikesService = new PostLikesService(userAccountService,userPostsService);
        savedPostsService = new SavedPostsService(userPostsService,userAccountService);
        userMessageService = new UserMessageService(userAccountService);

        userMenu = new UserMenu(userService);
        userAccountMenu = new UserAccountMenu(userAccountService);
        userPostsMenu = new UserPostsMenu(userPostsService);
        postCommentMenu = new PostCommentMenu(postCommentService,userPostsService);
        postLikeMenu = new PostLikeMenu(postLikesService,userPostsService);
        savedPostMenu = new SavedPostMenu(savedPostsService);
        userMessageMenu = new UserMessageMenu(userMessageService,userAccountService);
    }
    public async ValueTask Main()
    {
        bool circle = true;
        while (circle)
        {

            Console.Clear();
            var table = Selection.DataTable();
            AnsiConsole.Write(table);
            Console.Write(">>> :");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": await userMenu.DisplayAsync(); break;
                case "2": await userAccountMenu.DisplayAsync(); break;
                case "3": await userMessageMenu.DisplayAsync(); break;
                case "4": await userPostsMenu.DisplayAsync(); break;
                case "5": await postCommentMenu.DisplayAsync(); break;
                case "6": await postLikeMenu.DisplayAsync(); break;
                case "7": await savedPostMenu.DisplayAsync(); break;
                case "8": circle = false; break;
                default: Console.Clear(); Console.WriteLine("You have entered an invalid command !"); Thread.Sleep(800); break;
            }
        }
    }
}
