using DomainEntities;

namespace Repositories;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task<User> GetSingleAsync(int id);
    Task<int?> GetIdByUsernameAsync(string username);
    Task<IQueryable<User>> GetManyAsync();
    Task<User> GetSingleByUsernameAsync(string username);
}