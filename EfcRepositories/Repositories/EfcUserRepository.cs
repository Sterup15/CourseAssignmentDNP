using DomainEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repositories;

namespace EfcRepositories.Repositories;

public class EfcUserRepository : IUserRepository
{
    private readonly DatabaseContext _context;

    public EfcUserRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<User> AddAsync(User user)
    {
        EntityEntry<User> entityEntry = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(User user)
    {
        if (!(await _context.Users.AnyAsync(u => u.Id == user.Id)))
        {
            throw new Exception("User with id {user.Id} not found");
        }

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        User? existing = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
        if (existing == null)
        {
            throw new Exception("User with id {id} not found");
        }
        
        _context.Users.Remove(existing);
        await _context.SaveChangesAsync();
    }

    public async Task<User> GetSingleAsync(int id)
    {
        User? existing = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
        if (existing == null)
        {
            throw new Exception("User with id {id} not found");
        }

        return existing;
    }

    public async Task<int?> GetIdByUsernameAsync(string username)
    {
        User? existing = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        if (existing == null)
        {
            throw new Exception("User with username {username} not found");
        }

        return existing.Id; 
    }

    public async Task<IQueryable<User>> GetManyAsync()
    {
        return _context.Users;
    }

    public async Task<User> GetSingleByUsernameAsync(string username)
    {
        User? existing = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        if (existing == null)
        {
            throw new Exception("User with username {username} not found");
        }

        return existing; 
    }
}