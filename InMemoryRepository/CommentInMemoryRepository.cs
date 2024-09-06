using DomainEntities;
using Repositories;

namespace InMemoryRepository;

public class CommentInMemoryRepository : ICommentRepository
{
    List<Comment> comments = new List<Comment>();

    public CommentInMemoryRepository()
    {
        CreateDummyData();
    }

    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = comments.Any() 
            ? comments.Max(p => p.Id) + 1
            : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment)
    {
        Comment? existingComment = comments.SingleOrDefault(p => p.Id == comment.Id);
        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{comment.Id}' not found");
        }

        comments.Remove(existingComment);
        comments.Add(comment);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Comment? commentToRemove = comments.SingleOrDefault(p => p.Id == id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found");
        }

        comments.Remove(commentToRemove);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        Comment? comment = comments.SingleOrDefault(p => p.Id == id);
        if (comment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found");
        }
        
        return Task.FromResult(comment);
    }

    public IQueryable<Comment> GetMany()
    {
        return comments.AsQueryable();
    }

    private void CreateDummyData()
    {
        comments.Add(new Comment(1, "Du da sej hva", 1, 1));
        comments.Add(new Comment(2, "Sejt", 1, 2));
        comments.Add(new Comment(3, "Dumt", 2, 1));
        comments.Add(new Comment(4, "Ad", 3, 2));

    }
}