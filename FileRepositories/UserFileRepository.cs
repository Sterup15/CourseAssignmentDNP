using System.Text.Json;
using DomainEntities;
using Repositories;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "users.json";

    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    public async Task<User> AddAsync(User user)
    {
        List<User> users = ReadAndDeserializeAsync().Result;
        int maxId = users.Count > 0 ? users.Max(c => c.Id) : 1;
        user.Id = maxId + 1;
        users.Add(user);
        WriteAndSerializeAsync(users).Wait();
        return user;
    }

    public Task UpdateAsync(User user)
    {
        List<User> users = ReadAndDeserializeAsync().Result;
        User? existingUser = users.SingleOrDefault(p => p.Id == user.Id);
        if (existingUser is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{user.Id}' not found");
        }

        users.Remove(existingUser);
        users.Add(user);
        WriteAndSerializeAsync(users).Wait();
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        List<User> users = ReadAndDeserializeAsync().Result;
        User? userToRemove = users.SingleOrDefault(p => p.Id == id);
        if (userToRemove is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }

        users.Remove(userToRemove);
        WriteAndSerializeAsync(users).Wait();
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        List<User> users = ReadAndDeserializeAsync().Result;
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
        List<User> users = ReadAndDeserializeAsync().Result;
        return users.AsQueryable();
    }
    
    private async Task<List<User>> ReadAndDeserializeAsync()
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        return users;
    }

    private async Task<List<User>> WriteAndSerializeAsync(List<User> users)
    {
        String usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
        return users;
    }
}