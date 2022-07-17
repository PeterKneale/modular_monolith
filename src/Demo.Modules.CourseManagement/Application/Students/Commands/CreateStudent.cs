using Demo.Modules.CourseManagement.Domain.Common;
using Demo.Modules.CourseManagement.Domain.Students;

namespace Demo.Modules.CourseManagement.Application.Students.Commands;

public static class CreateStudent
{
    public record Command(Guid StudentId, string FirstName, string LastName) : IRequest;
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.StudentId).NotEmpty();
            RuleFor(m => m.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(m => m.LastName).NotEmpty().MaximumLength(50);
        }
    }
    
    public class Handler : IRequestHandler<Command>
    {
        private readonly IStudentRepository _students;
    
        public Handler(IStudentRepository students)
        {
            _students = students;
        }
    
        public async Task<Unit> Handle(Command command, CancellationToken token)
        {
            var studentid = StudentId.CreateInstance(command.StudentId);
            var name = new Name(command.FirstName, command.LastName);
    
            var student = Student.CreateInstance(studentid, name);
    
            await _students.Add(student);
    
            return Unit.Value;
        }
    }
}
