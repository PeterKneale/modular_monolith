using Demo.Modules.CourseManagement.Domain.Teachers;

namespace Demo.Modules.CourseManagement.Application.Contracts;

public interface ITeacherRepository
{
    Task Add(Teacher teacher);
    Task<IEnumerable<Teacher>> List();
    Task<Teacher?> Get(TeacherId teacherId);
}