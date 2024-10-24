using System.Text.Json;
using DomainEntities;
using Repositories;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private readonly string filePath = "comments.json";
        
    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    
    public async Task<Comment> AddAsync(Comment comment)
    {
        List<Comment> comments = ReadAndDeserializeAsync().Result;
        int maxId = comments.Count > 0 ? comments.Max(c => c.Id) : 1;
        comment.Id = maxId + 1;
        comments.Add(comment);
        WriteAndSerializeAsync(comments).Wait();
        return comment;
    }

    public async Task<Comment> UpdateAsync(Comment comment)
    {
        List<Comment> comments = ReadAndDeserializeAsync().Result;
        Comment? existingComment = comments.SingleOrDefault(p => p.Id == comment.Id);
        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{comment.Id}' not found");
        }

        comments.Remove(existingComment);
        comments.Add(comment);
        WriteAndSerializeAsync(comments).Wait();
        return comment;
    }

    public Task DeleteAsync(int id)
    {
        List<Comment> comments = ReadAndDeserializeAsync().Result;
        Comment? commentToRemove = comments.SingleOrDefault(p => p.Id == id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found");
        }

        comments.Remove(commentToRemove);
        WriteAndSerializeAsync(comments).Wait();
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        List<Comment> comments = ReadAndDeserializeAsync().Result;
        Comment? comment = comments.SingleOrDefault(p => p.Id == id);
        if (comment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found");
        }
        
        return Task.FromResult(comment);
    }

    public async Task<IQueryable<Comment>> GetManyAsync()
    {
        List<Comment> comments = await ReadAndDeserializeAsync();
        return comments.AsQueryable();
    }

    private async Task<List<Comment>> ReadAndDeserializeAsync()
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        return comments;
    }

    private async Task<List<Comment>> WriteAndSerializeAsync(List<Comment> comments)
    {
        String commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
        return comments;
    }
}