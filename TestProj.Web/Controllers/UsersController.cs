using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TestProj.Domain.Interfaces;
using TestProj.Models;

namespace TestProj.Web.Controllers;

[SwaggerTag("Управление текущим пользователем")]
[ApiController]
[Route("api/users")]
[Produces("application/json")]
public class UsersController([FromServices] IUserService userService) : ControllerBase
{
    [HttpPost("add")]
    public async Task AddUser(UserDto userDto)
    {
        await userService.AddUser(userDto);
    }
    
    [HttpGet("get")]
    public async Task<UserDto> GetUser([FromQuery] string username)
    {
        return await userService.GetUser(username);
    }
    
    [HttpDelete("delete/{username}")]
    public async Task DeleteUser([FromRoute] string username)
    {
        await userService.DeleteUser(username);
    }
}