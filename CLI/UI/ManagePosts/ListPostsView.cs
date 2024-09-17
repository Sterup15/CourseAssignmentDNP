using DomainEntities;
using Repositories;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private IPostRepository PostRepository { get; set; }

    public ListPostsView(IPostRepository postRepository)
    {
        PostRepository = postRepository;
    }

    public Task<IQueryable<Post>> ListPosts()
    {
        foreach (Post post in PostRepository.GetMany())
        {
            Console.WriteLine($"{post.Title}: {post.Body}");
            Console.WriteLine($"Written by: {post.UserId}");
            Console.WriteLine();
        }

        return Task.FromResult<IQueryable<Post>>(null!);
    }
}