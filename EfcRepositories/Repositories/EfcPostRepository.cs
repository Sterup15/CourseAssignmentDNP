using DomainEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repositories;

namespace EfcRepositories.Repositories;

public class EfcPostRepository : IPostRepository
{
    private readonly DatabaseContext _context;

    public EfcPostRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Post> AddAsync(Post post)
    {
        EntityEntry<Post> entityEntry = await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(Post post)
    {
        if (!(await _context.Posts.AnyAsync(p => p.Id == post.Id)))
        {
            throw new Exception($"Post with id {post.Id} not found");
        }

        _context.Posts.Update(post);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Post? existing = await _context.Posts.SingleOrDefaultAsync(p => p.Id == id);
        if (existing == null)
        {
            throw new Exception($"Post with id {id} not found");
        }

        _context.Posts.Remove(existing);
        await _context.SaveChangesAsync();
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        Post? existing = await _context.Posts.SingleOrDefaultAsync(p => p.Id == id);
        if (existing == null)
        {
            throw new Exception($"Post with id {id} not found");
        }

        return existing;
    }

    public async Task<IQueryable<Post>> GetManyAsync()
    {
        return _context.Posts;
    }
}