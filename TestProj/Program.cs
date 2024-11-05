using TestProj.Models;
using TestProj.Postgres;

namespace TestProj;

class Program
{
    static void Main(string[] args)
    {
        using ApplicationContext context = new ApplicationContext();

        context.Users.Add(new DbUser() { Age = 12, Id = Guid.NewGuid(), Name = "John Doe" });
        
        context.SaveChanges();

        foreach (var user in context.Users)
        {
            Console.WriteLine($"{user.Id}, {user.Name}, {user.Age}");
        }
    }
}