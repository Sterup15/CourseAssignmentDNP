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

    public async Task<IQueryable<Post>> ListPosts()
    {
        IQueryable<Post> posts = await PostRepository.GetManyAsync();
        foreach (Post post in posts)
        {
            Console.WriteLine($"{post.Title}: {post.Body}");
            Console.WriteLine($"Written by: {post.UserId}");
            Console.WriteLine();
        }

        return await Task.FromResult<IQueryable<Post>>(null!);
    }
}