using Repositories;

namespace CLI.UI;

public class CliApp
{
    private IPostRepository PostRepository { get; set; }
    private IUserRepository UserRepository { get; set; }
    private ICommentRepository CommentRepository { get; set; }

    public CliApp(IPostRepository postRepository, IUserRepository userRepository, ICommentRepository commentRepository)
    {
        PostRepository = postRepository;
        UserRepository = userRepository;
        CommentRepository = commentRepository;
    }

    public async Task StartAsync()
    {
        
    }
}