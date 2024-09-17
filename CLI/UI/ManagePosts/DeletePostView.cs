using DomainEntities;
using Repositories;

namespace CLI.UI.ManagePosts;

public class DeletePostView
{
    private IPostRepository PostRepository { get; set; }

    public DeletePostView(IPostRepository postRepository)
    {
        PostRepository = postRepository;
    }

    public async Task DeletePost(int postId)
    {
        await PostRepository.DeleteAsync(postId);
    }
}