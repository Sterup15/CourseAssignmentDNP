namespace WebApi.Services;

using DomainEntities;
using Repositories;
using ApiContracts.DTOs;

public class UserService
{
    private readonly IUserRepository userRepo;

    public UserService(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }

    public async Task<UserDto> CreateUserAsync(CreateOrUpdateUserDto request)
    {
        // Business logic for creating a new user
        User user = new(request.Username, request.Password);
        User created = await userRepo.AddAsync(user);
        
        return new UserDto
        {
            Id = created.Id,
            UserName = created.Username
        };
    }

    public async Task<UserDto> UpdateUserAsync(int id, CreateOrUpdateUserDto request)
    {
        // Business logic for updating an existing user
        User updatedUser = new(id, request.Username, request.Password);
        await userRepo.UpdateAsync(updatedUser);
        
        return new UserDto
        {
            Id = updatedUser.Id,
            UserName = updatedUser.Username
        };
    }

    public async Task<UserDto> GetSingleUserAsync(int id)
    {
        // Business logic for getting a single user by ID
        User user = await userRepo.GetSingleAsync(id);
        
        return new UserDto
        {
            Id = user.Id,
            UserName = user.Username
        };
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync(string filterByString)
    {
        // Business logic for getting and filtering users
        IQueryable<User> users = await userRepo.GetManyAsync();

        if (!string.IsNullOrEmpty(filterByString))
        {
            users = users.Where(u => u.Username.Contains(filterByString));
        }

        // Project to DTOs
        var userDtos = users.Select(u => new UserDto
        {
            Id = u.Id,
            UserName = u.Username
        }).ToList();

        return userDtos;
    }

    public async Task DeleteUserAsync(int id)
    {
        // Business logic for deleting a user
        await userRepo.DeleteAsync(id);
    }
}