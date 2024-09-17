using DomainEntities;
using Repositories;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private IPostRepository PostRepository { get; set; }

    public CreatePostView(IPostRepository postRepository)
    {
        PostRepository = postRepository;
    }

    public async Task CreatePost(string[] args)
    {
        await PostRepository.AddAsync(new Post(Convert.ToInt32(args[0]), args[1], args[2], Convert.ToInt32(args[3])));
    }
}