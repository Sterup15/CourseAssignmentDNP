using DomainEntities;
using Repositories;

namespace InMemoryRepository;

public class PostInMemoryRepository : IPostRepository
{

    public List<Post> posts = new List<Post>();

    public PostInMemoryRepository()
    {
        CreateDummyData();
    }

    public Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Any() 
            ? posts.Max(p => p.Id) + 1
            : 1;
        posts.Add(post);
        Console.WriteLine($"Post {post.Id} added");
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{post.Id}' not found");
        }

        posts.Remove(existingPost);
        posts.Add(post);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Post? postToRemove = posts.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        posts.Remove(postToRemove);
        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsync(int id)
    {
        Post? post = posts.SingleOrDefault(p => p.Id == id);
        if (post is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }
        
        return Task.FromResult(post);
    }

    public IQueryable<Post> GetMany()
    {
        return posts.AsQueryable();
    }

    public void CreateDummyData()
    {
        posts.Add(new Post(1, "Jeg er sej", "Jeg er bare så sejt", 1));
        posts.Add(new Post(2, "Jeg er ikke sej", "Jeg er bare ikke særlig sejt", 2));
        posts.Add(new Post(3, "Jeg er mere sej", "Jeg er bare så meget mere sejt", 3));
    }
}