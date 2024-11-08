using ApiContracts.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserService userService;

    public AuthController(UserService userService)
    {
        this.userService = userService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Authenticate([FromBody] CreateOrUpdateUserDto loginInfo)
    {
        CreateOrUpdateUserDto userFromDb = await userService.GetSingleUserByUsernameAsync(loginInfo.Username);
        if (userFromDb == null)
        {
            return Unauthorized("Invalid username");
        }

        if (loginInfo.Password != userFromDb.Password)
        {
            return Unauthorized("Invalid password");
        }

        var UserDto = new UserDto
        {
            Id = await userService.GetIdByUsernameAsync(userFromDb.Username),
            UserName = userFromDb.Username
        };
        
        return Ok(UserDto);
    }
}