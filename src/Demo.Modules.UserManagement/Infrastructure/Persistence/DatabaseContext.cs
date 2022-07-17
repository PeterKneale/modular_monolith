using Demo.Modules.UserManagement.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Demo.Modules.UserManagement.Infrastructure.Persistence;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}