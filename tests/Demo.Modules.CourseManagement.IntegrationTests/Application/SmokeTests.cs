using Demo.Modules.Common;
using Demo.Modules.CourseManagement.Application.Categories.Commands;
using Demo.Modules.CourseManagement.Application.Courses.Commands;
using Demo.Modules.CourseManagement.Application.Courses.Queries;
using Demo.Modules.CourseManagement.Application.Students.Commands;
using Demo.Modules.CourseManagement.Application.Teachers.Commands;

namespace Demo.Modules.CourseManagement.IntegrationTests.Application;

[Collection(nameof(ModuleCollection))]
public class SmokeTests : IClassFixture<ModuleFixture>
{
    private readonly IModule _module;

    public SmokeTests(ModuleFixture module, ITestOutputHelper outputHelper)
    {
        module.OutputHelper = outputHelper;
        _module = new CourseManagementModule();
    }

    [Fact]
    public async Task Course_created_with_command_can_be_retrieved_with_query()
    {
        // arrange
        var teacherId = Guid.NewGuid();
        var teacherFirstName = "john";
        var teacherLastName = "smith";
        var studentId = Guid.NewGuid();
        var studentFirstName = "john";
        var studentLastName = "smith";
        var categoryId = Guid.NewGuid();
        var categoryTitle = "programming";
        var categoryDescription = null as string;
        var courseId = Guid.NewGuid();
        var courseTitle = "intro to C#";
        var courseDescription = "an introductory course";

        // act
        await _module.SendCommand(new CreateTeacher.Command(teacherId, teacherFirstName, teacherLastName));
        await _module.SendCommand(new CreateStudent.Command(studentId, studentFirstName, studentLastName));
        await _module.SendCommand(new CreateCategory.Command(categoryId, categoryTitle, categoryDescription));
        await _module.SendCommand(new CreateCourse.Command(courseId, teacherId, categoryId, courseTitle, courseDescription));
        await _module.SendCommand(new EnrollInCourse.Command(courseId, studentId));

        // assert
        var results0 = await _module.SendQuery(new ListCourses.Query());
        results0.Courses.Should().BeEquivalentTo(new ListCourses.Model[] {new(courseId, courseTitle, categoryTitle, 1L)});
        
        var results1 = await _module.SendQuery(new ListCoursesByTeacher.Query(teacherId));
        results1.Courses.Should().BeEquivalentTo(new ListCoursesByTeacher.Model[] {new(courseId, courseTitle, categoryTitle)});
        
        var results2 = await _module.SendQuery(new ListCoursesByStudent.Query(studentId));
        results2.Courses.Should().BeEquivalentTo(new ListCoursesByStudent.Model[] {new(courseId, courseTitle, categoryTitle)});
    }
}