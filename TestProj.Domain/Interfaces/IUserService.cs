using TestProj.Models;

namespace TestProj.Domain.Interfaces;

public interface IUserService
{
    Task AddUser(UserDto user);
    
    Task<UserDto> GetUser(string username);
    
    Task DeleteUser(string username);
}