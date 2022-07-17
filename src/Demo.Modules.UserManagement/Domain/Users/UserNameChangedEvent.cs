namespace Demo.Modules.UserManagement.Domain.Users;

public class UserNameChangedEvent : BaseEvent
{
    public User User { get; }

    public UserNameChangedEvent(User student)
    {
        User = student;
    }
}