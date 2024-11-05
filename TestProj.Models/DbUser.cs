using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestProj.Models;

public class DbUser
{
    [Key]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
}

public class DbUserConfiguration : IEntityTypeConfiguration<DbUser>
{
    public void Configure(EntityTypeBuilder<DbUser> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
    }
}