namespace Demo.Modules.UserManagement.Domain.Users;

public class UserId : BaseValueObject
{
    public UserId()
    {
        
    }
    private UserId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("User id cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static UserId CreateInstance(Guid value) => new(value);

    public Guid Value { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}