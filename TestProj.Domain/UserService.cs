using Microsoft.EntityFrameworkCore;
using TestProj.Domain.Interfaces;
using TestProj.Mappers;
using TestProj.Models;
using TestProj.Postgres;

namespace TestProj.Domain;

public class UserService(ApplicationContext applicationContext) : IUserService
{
    public async Task AddUser(UserDto user)
    {
        await applicationContext.AddAsync(DbUserMapper.Map(user));
        
        await applicationContext.SaveChangesAsync();
    }

    public async Task<UserDto> GetUser(string username)
    {
        var dbUser = await applicationContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Name == username);

        if (dbUser == null)
        {
            throw new Exception($"User {username} not found");
        }
        
        return UserDtoMapper.Map(dbUser);
    }

    public async Task DeleteUser(string username)
    {
        var dbUser = await applicationContext.Users.FirstOrDefaultAsync(u => u.Name == username);

        if (dbUser == null)
        {
            throw new Exception($"User {username} not found");
        }
        
        applicationContext.Users.Remove(dbUser);
        
        await applicationContext.SaveChangesAsync();
    }
}