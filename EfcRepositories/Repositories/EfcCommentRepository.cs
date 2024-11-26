using DomainEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repositories;

namespace EfcRepositories.Repositories;

public class EfcCommentRepository : ICommentRepository
{
    private readonly DatabaseContext _context;

    public EfcCommentRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        EntityEntry<Comment> entityEntry = await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<Comment> UpdateAsync(Comment comment)
    {
        if (!(await _context.Comments.AnyAsync(c => c.Id == comment.Id)))
        {
            throw new Exception($"Comment with id {comment.Id} not found");
        }

        _context.Comments.Update(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task DeleteAsync(int id)
    {
        Comment? existing = await _context.Comments.SingleOrDefaultAsync(c => c.Id == id);
        if (existing == null)
        {
            throw new Exception($"Comment with id {id} not found");
        }

        _context.Comments.Remove(existing);
        await _context.SaveChangesAsync();
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        Comment? existing = await _context.Comments.SingleOrDefaultAsync(c => c.Id == id);
        if (existing == null)
        {
            throw new Exception($"Comment with id {id} not found");
        }

        return existing;
    }

    public async Task<IQueryable<Comment>> GetManyAsync()
    {
        return _context.Comments;
    }
}