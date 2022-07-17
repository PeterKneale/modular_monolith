using Demo.Modules.UserManagement.Domain.Common;

namespace Demo.Modules.UserManagement.Domain.Users;

public class User : BaseEntity
{
    public UserId Id { get; private init; }

    public Name Name { get; private set; }
    
    private User()
    {
    }

    public User(UserId id, Name name)
    {
        Id = id;
        Name = name;
    }

    public void ChangeName(Name name)
    {
        Name = name;
        AddDomainEvent(new UserNameChangedEvent(this));
    }

    public static User CreateInstance(UserId studentId, Name name) => new(studentId, name);
}