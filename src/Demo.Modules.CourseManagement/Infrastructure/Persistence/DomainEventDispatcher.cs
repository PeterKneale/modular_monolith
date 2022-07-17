using Demo.Modules.CourseManagement.Domain.Students;

namespace Demo.Modules.CourseManagement.Infrastructure.Persistence;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;
    
    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task DispatchDomainEvents(DatabaseContext db)
    {
        var entities = db.ChangeTracker
            .Entries<Student>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);
        
        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();
            
        entities
            .ToList()
            .ForEach(e => e.ClearDomainEvents());
        
        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent);
        }
    }
}