using TestProj.Models;

namespace TestProj.Mappers;

public static class UserDtoMapper
{
    public static UserDto Map(DbUser user)
    {
        return new UserDto()
        {
            Name = user.Name,
            Age = user.Age,
        };
    }
}