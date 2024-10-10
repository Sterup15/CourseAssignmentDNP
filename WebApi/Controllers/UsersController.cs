using ApiContracts.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService userService;

        public UsersController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateOrUpdateUserDto request)
        {
            // Delegate to service
            UserDto dto = await userService.CreateUserAsync(request);
            return Created($"api/users/{dto.Id}", dto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserDto>> Update(int id, [FromBody] CreateOrUpdateUserDto request)
        {
            // Delegate to service
            UserDto dto = await userService.UpdateUserAsync(id, request);
            return Created($"api/users/{dto.Id}", dto);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> GetSingle(int id)
        {
            // Delegate to service
            UserDto dto = await userService.GetSingleUserAsync(id);
            return Ok(dto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetMany(string filterByString)
        {
            // Delegate to service
            var userDtos = await userService.GetUsersAsync(filterByString);

            // Return 404 if no users found
            if (!userDtos.Any())
            {
                return NotFound();
            }

            return Ok(userDtos);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Delegate to service
            await userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}