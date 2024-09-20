using CLI.UI;
using FileRepositories;
using Repositories;

namespace CLI;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting CLI app...");
        IUserRepository userRepository = new UserFileRepository();
        IPostRepository postRepository = new PostFileRepository();
        ICommentRepository commentRepository = new CommentFileRepository();

        CliApp cliApp = new CliApp(postRepository, userRepository, commentRepository);
        await cliApp.StartAsync();
    }
}