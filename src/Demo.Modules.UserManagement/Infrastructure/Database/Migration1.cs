using FluentMigrator;

namespace Demo.Modules.UserManagement.Infrastructure.Database;

[Migration(1)]
public class Migration1 : Migration
{
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("first_name").AsString()
            .WithColumn("last_name").AsString()
            .WithColumn("email").AsString()
            .WithColumn("password").AsString();
    }

    public override void Down()
    {
        DropTableIfExists("users");
    }
    
    private void DropTableIfExists(string tableName)
    {
        Execute.Sql($"DROP TABLE IF EXISTS {tableName};");
    }
}