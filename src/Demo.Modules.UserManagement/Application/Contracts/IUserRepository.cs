using Demo.Modules.UserManagement.Domain.Users;

namespace Demo.Modules.UserManagement.Application.Contracts;

public interface IUserRepository
{
    Task Add(User student);
    Task<IEnumerable<User>> List();
    Task<User?> Get(UserId studentId);
}