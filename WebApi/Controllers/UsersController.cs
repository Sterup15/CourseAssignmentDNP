using ApiContracts.DTOs;
using DomainEntities;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepo;

    public UsersController(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto request)
    {
        User user = new(request.Username, request.Password);
        User created = await userRepo.AddAsync(user);
        UserDto dto = new()
        {
            Id = created.Id,
            UserName = created.Username
        };
        return Created($"api/users/{dto.Id}", dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> Update(int id, [FromBody] UpdateUserDto request)
    {
        User updatedUser = new(id, request.Username, request.Password);
        await userRepo.UpdateAsync(updatedUser);
        UserDto dto = new()
        {
            Id = updatedUser.Id,
            UserName = updatedUser.Username
        };
        return Created($"api/users/{dto.Id}", dto);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetSingle(int id)
    {
        User user = await userRepo.GetSingleAsync(id);
        UserDto dto = new()
        {
            Id = user.Id,
            UserName = user.Username
        };
        return dto;
    }
    
    [HttpGet]
    public IEnumerable<UserDto> GetMany(string filterByString)
    {
        IQueryable<User> users = userRepo.GetMany();

        // Apply filtering using LINQ
        if (!string.IsNullOrEmpty(filterByString))
        {
            users = users.Where(u => u.Username.Contains(filterByString));
        }

        // Project the results to UserDto and return as a list
        return users.Select(u => new UserDto
        {
            Id = u.Id,
            UserName = u.Username
        }).ToList(); // Use ToList() to materialize the query
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await userRepo.DeleteAsync(id);

        // Return 204 No Content to indicate successful deletion
        return NoContent();
    }

}
