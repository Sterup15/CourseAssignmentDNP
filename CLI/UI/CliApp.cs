using CLI.UI.ManageComments;
using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using Repositories;

namespace CLI.UI;

public class CliApp
{
    private ManagePostsView ManagePostsView { get; set; }
    private ManageUsersView ManageUsersView { get; set; }
    private ManageCommentsView ManageCommentsView { get; set; }

    public CliApp(IPostRepository postRepository, IUserRepository userRepository, ICommentRepository commentRepository)
    {
        ManagePostsView = new ManagePostsView(postRepository);
        ManageUsersView = new ManageUsersView(userRepository);
        ManageCommentsView = new ManageCommentsView(commentRepository);
    }

    public async Task StartAsync()
    {
        Console.WriteLine("Enter a command (type 'exit' to quit):");

        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input)) continue;

            if (input.ToLower() == "exit")
            {
                Console.WriteLine("Exiting...");
                break;
            }

            string[] args = input.Split(' ');

            switch (args[0].ToLower())
            {
                case "post":
                    await ManagePostsView.HandleAsync(args[1..]);
                    break;
                case "user":
                    await ManageUsersView.HandleAsync(args[1..]);
                    break;
                case "comment":
                    await ManageCommentsView.HandleAsync(args[1..]);
                    break;
                default:
                    Console.WriteLine("Unknown command. Use 'post', 'user', or 'comment'.");
                    break;
            }
        }
        
    }
}