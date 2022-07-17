namespace Demo.Modules.CourseManagement.Infrastructure.Persistence;

public interface IDomainEventDispatcher
{
    Task DispatchDomainEvents(DatabaseContext db);
}