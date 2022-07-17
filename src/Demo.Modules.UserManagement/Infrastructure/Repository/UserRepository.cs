using Demo.Modules.UserManagement.Application.Contracts;
using Demo.Modules.UserManagement.Domain.Users;
using Demo.Modules.UserManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Demo.Modules.UserManagement.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _db;

    public UserRepository(DatabaseContext db)
    {
        _db = db;
    }

    public async Task Add(User student)
    {
        await _db.Users.AddAsync(student);
    }

    public async Task<IEnumerable<User>> List()
    {
        return await _db.Users.ToListAsync();
    }

    public async Task<User?> Get(UserId studentId)
    {
        return await _db.Users.FindAsync(studentId);
    }
}