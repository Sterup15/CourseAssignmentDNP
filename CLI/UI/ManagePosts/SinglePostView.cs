using Repositories;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private IPostRepository PostRepository { get; set; }

    public SinglePostView(IPostRepository postRepository)
    {
        PostRepository = postRepository;
    }
}