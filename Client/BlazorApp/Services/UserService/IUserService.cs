using ApiContracts.DTOs;

namespace BlazorApp.Services.UserService;

public interface IUserService
{
        Task<UserDto> CreateUserAsync(CreateOrUpdateUserDto request);
        Task<UserDto> UpdateUserAsync(int id, CreateOrUpdateUserDto request);
        Task<UserDto> GetSingleUserAsync(int id);
        Task<IEnumerable<UserDto>> GetUsersAsync(string filterByString);
        Task DeleteUserAsync(int id);
    
}