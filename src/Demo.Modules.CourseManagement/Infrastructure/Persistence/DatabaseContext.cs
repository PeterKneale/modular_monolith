using Demo.Modules.CourseManagement.Domain.Categories;
using Demo.Modules.CourseManagement.Domain.Courses;
using Demo.Modules.CourseManagement.Domain.Students;
using Demo.Modules.CourseManagement.Domain.Teachers;

namespace Demo.Modules.CourseManagement.Infrastructure.Persistence;

public class DatabaseContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Category> Categories { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}