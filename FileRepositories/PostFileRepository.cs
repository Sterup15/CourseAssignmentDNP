using System.Text.Json;
using DomainEntities;
using Repositories;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    private readonly string filePath = "posts.json";

    public PostFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    public async Task<Post> AddAsync(Post post)
    {
        List<Post> posts = ReadAndDeserializeAsync().Result;
        int maxId = posts.Count > 0 ? posts.Max(c => c.Id) : 1;
        post.Id = maxId + 1;
        posts.Add(post);
        WriteAndSerializeAsync(posts).Wait();
        return post;
    }

    public Task UpdateAsync(Post post)
    {
        List<Post> posts = ReadAndDeserializeAsync().Result;
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{post.Id}' not found");
        }

        posts.Remove(existingPost);
        posts.Add(post);
        WriteAndSerializeAsync(posts).Wait();
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        List<Post> posts = ReadAndDeserializeAsync().Result;
        Post? postToRemove = posts.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        posts.Remove(postToRemove);
        WriteAndSerializeAsync(posts).Wait();
        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsync(int id)
    {
        List<Post> posts = ReadAndDeserializeAsync().Result;
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
        List<Post> posts = ReadAndDeserializeAsync().Result;
        return posts.AsQueryable();
    }
    
    private async Task<List<Post>> ReadAndDeserializeAsync()
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
        return posts;
    }

    private async Task<List<Post>> WriteAndSerializeAsync(List<Post> posts)
    {
        String postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postsAsJson);
        return posts;
    }
}