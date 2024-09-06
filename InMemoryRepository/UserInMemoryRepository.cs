using DomainEntities;
using Repositories;

namespace InMemoryRepository;

public class UserInMemoryRepository : IUserRepository
{
    
    List<User> users = new List<User>();

    public UserInMemoryRepository()
    {
        CreateDummyData();
    }

    public Task<User> AddAsync(User user)
    {
        user.Id = users.Any() 
            ? users.Max(p => p.Id) + 1
            : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        User? existingUser = users.SingleOrDefault(p => p.Id == user.Id);
        if (existingUser is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{user.Id}' not found");
        }

        users.Remove(existingUser);
        users.Add(user);

        return Task.CompletedTask;    }

    public Task DeleteAsync(int id)
    {
        User? userToRemove = users.SingleOrDefault(p => p.Id == id);
        if (userToRemove is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }

        users.Remove(userToRemove);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        User? user = users.SingleOrDefault(p => p.Id == id);
        if (user is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }
        
        return Task.FromResult(user);
    }

    public IQueryable<User> GetMany()
    {
        return users.AsQueryable();
    }

    public void CreateDummyData()
    {
        users.Add(new User(1, "Jørgen", "sejt123"));
        users.Add(new User(2, "Torben", "sejt132"));
        users.Add(new User(3, "Else", "ikkesåsejt123"));

    }
}