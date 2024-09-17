using Repositories;

namespace CLI.UI.ManageComments;

public class ManageCommentsView
{
    private ICommentRepository CommentRepository { get; set; }

    public ManageCommentsView(ICommentRepository commentRepository)
    {
        CommentRepository = commentRepository;
    }

    public async Task HandleAsync(string[] args)
    {
        /*
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a comment command (create, list, delete).");
            return;
        }

        switch (args[0].ToLower())
        {
            case "create":
                await CreateCommentAsync(args);
                break;
            case "list":
                ListComments();
                break;
            case "delete":
                if (args.Length > 1 && int.TryParse(args[1], out var commentId))
                {
                    DeleteComment(commentId);
                }
                else
                {
                    Console.WriteLine("Please provide a valid comment ID to delete.");
                }
                break;
            default:
                Console.WriteLine("Unknown comment command.");
                break;
        }
        */
    }
}