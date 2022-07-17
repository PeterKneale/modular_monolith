using Demo.Modules.CourseManagement.Domain.Students;

namespace Demo.Modules.CourseManagement.Application.Contracts;

public interface IStudentRepository
{
    Task Add(Student student);
    Task<IEnumerable<Student>> List();
    Task<Student?> Get(StudentId studentId);
}