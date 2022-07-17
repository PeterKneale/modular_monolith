using Demo.Modules.CourseManagement.Application.Contracts;
using Demo.Modules.CourseManagement.Domain.Common;
using Demo.Modules.CourseManagement.Domain.Students;
using Demo.Modules.CourseManagement.Infrastructure.Persistence;

namespace Demo.Modules.CourseManagement.IntegrationTests.Infrastructure;

[Collection(nameof(ModuleCollection))]
public class SmokeTests : IClassFixture<ModuleFixture>
{
    public SmokeTests(ModuleFixture module, ITestOutputHelper outputHelper)
    {
        module.OutputHelper = outputHelper;
    }

    [Fact]
    public async Task Added_student_can_be_retrieved_with_get()
    {
        // arrange
        using var scope1 = CompositionRoot.BeginLifetimeScope();
        var repo = scope1.ServiceProvider.GetRequiredService<IStudentRepository>();
        var db = scope1.ServiceProvider.GetRequiredService<DatabaseContext>();
        var studentId = StudentId.CreateInstance(Guid.NewGuid());
        var name = new Name("john", "smith");

        // act
        var student = Student.CreateInstance(studentId, name);
        await repo.Add(student);
        await db.SaveChangesAsync();

        // assert
        using var scope2 = CompositionRoot.BeginLifetimeScope();
        var repo2 = scope2.ServiceProvider.GetRequiredService<IStudentRepository>();
        var student2 = await repo2.Get(studentId);
        student2.Should().BeEquivalentTo(student);
    }
    
    [Fact]
    public async Task Added_student_can_be_found_in_list()
    {
        // arrange
        using var scope1 = CompositionRoot.BeginLifetimeScope();
        var repo = scope1.ServiceProvider.GetRequiredService<IStudentRepository>();
        var db = scope1.ServiceProvider.GetRequiredService<DatabaseContext>();
        var studentId = StudentId.CreateInstance(Guid.NewGuid());
        var name = new Name("john","smith");

        // act
        var student = Student.CreateInstance(studentId, name);
        await repo.Add(student);
        await db.SaveChangesAsync();
        
        // assert
        using var scope2 = CompositionRoot.BeginLifetimeScope();
        var repo2 = scope2.ServiceProvider.GetRequiredService<IStudentRepository>();
        var list = await repo2.List();
        list.Should().ContainEquivalentOf(student);
    }
}