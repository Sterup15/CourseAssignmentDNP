using CLI.UI;
using InMemoryRepository;
using Repositories;

namespace CLI;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting CLI app...");
        IUserRepository userRepository = new UserInMemoryRepository();
        IPostRepository postRepository = new PostInMemoryRepository();
        ICommentRepository commentRepository = new CommentInMemoryRepository();

        CliApp cliApp = new CliApp(postRepository, userRepository, commentRepository);
        await cliApp.StartAsync();
    }
}