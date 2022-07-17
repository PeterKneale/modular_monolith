using Demo.Modules.CourseManagement.Domain.Categories;
using Demo.Modules.CourseManagement.Domain.Courses;
using Demo.Modules.CourseManagement.Domain.Teachers;
using Details = Demo.Modules.CourseManagement.Domain.Courses.Details;

namespace Demo.Modules.CourseManagement.Application.Courses.Commands;

public static class CreateCourse
{
    public record Command(Guid CourseId, Guid TeacherId, Guid CategoryId, string Title, string? Description) : IRequest;
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.CourseId).NotEmpty();
            RuleFor(m => m.TeacherId).NotEmpty();
            RuleFor(m => m.CategoryId).NotEmpty();
            RuleFor(m => m.Title).NotEmpty().MaximumLength(50);
            RuleFor(m => m.Description).MaximumLength(50);
        }
    }
    
    public class Handler : IRequestHandler<Command>
    {
        private readonly ICourseRepository _courses;
    
        public Handler(ICourseRepository courses)
        {
            _courses = courses;
        }
    
        public async Task<Unit> Handle(Command command, CancellationToken token)
        {
            var courseId = CourseId.CreateInstance(command.CourseId);
            var teacherId = TeacherId.CreateInstance(command.TeacherId);
            var categoryId = CategoryId.CreateInstance(command.CategoryId);
            var description = new Details(command.Title, command.Description);
    
            var course = Course.CreateInstance(courseId, teacherId, categoryId, description);
    
            await _courses.Add(course);
    
            return Unit.Value;
        }
    }
}