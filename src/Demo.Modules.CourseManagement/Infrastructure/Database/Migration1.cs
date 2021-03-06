using FluentMigrator;

namespace Demo.Modules.CourseManagement.Infrastructure.Database;

[Migration(1)]
public class Migration1 : Migration
{
    public override void Up()
    {
        Create.Table("students")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("first_name").AsString()
            .WithColumn("last_name").AsString();

        Create.Table("teachers")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("first_name").AsString()
            .WithColumn("last_name").AsString();
        
        Create.Table("courses")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("teacher_id").AsGuid()
            .WithColumn("category_id").AsGuid()
            .WithColumn("title").AsString()
            .WithColumn("description").AsString().Nullable();
        
        Create.Table("enrollments")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("student_id").AsGuid()
            .WithColumn("course_id").AsGuid()
            .WithColumn("is_active").AsBoolean()
            .WithColumn("enrolled_at").AsDateTime()
            .WithColumn("unenrolled_at").AsDateTime().Nullable();
        
        Create.Table("categories")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("title").AsString()
            .WithColumn("description").AsString().Nullable();
    }

    public override void Down()
    {
        DropTableIfExists("enrollments");
        DropTableIfExists("categories");
        DropTableIfExists("courses");
        DropTableIfExists("students");
        DropTableIfExists("teachers");
    }
    
    private void DropTableIfExists(string tableName)
    {
        Execute.Sql($"DROP TABLE IF EXISTS {tableName};");
    }
}