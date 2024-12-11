using TestProj.Models;

namespace TestProj.Mappers;

public class DbUserMapper
{
    public static DbUser Map(UserDto user)
    {
        return new DbUser()
        {
            Id = Guid.NewGuid(),
            Name = user.Name,
            Age = user.Age,
        };
    }
}