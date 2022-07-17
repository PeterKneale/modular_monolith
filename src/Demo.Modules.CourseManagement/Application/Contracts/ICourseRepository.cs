using Demo.Modules.CourseManagement.Domain.Courses;

namespace Demo.Modules.CourseManagement.Application.Contracts;

public interface ICourseRepository
{
    Task Add(Course course);
    Task<Course?> Get(CourseId courseId);
}