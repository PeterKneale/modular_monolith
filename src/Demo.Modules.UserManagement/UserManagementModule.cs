using Demo.Modules.Common;

namespace Demo.Modules.UserManagement;

public interface IUserManagementModule : IModule
{

}
public class UserManagementModule : IUserManagementModule
{
    public async Task SendCommand(IRequest command)
    {
        using var scope = CompositionRoot.BeginLifetimeScope();
        var dispatcher = scope.ServiceProvider.GetRequiredService<IMediator>();
        await dispatcher.Send(command);
    }
    
    public async Task<TResult> SendQuery<TResult>(IRequest<TResult> query)
    { 
        using var scope = CompositionRoot.BeginLifetimeScope();
        var dispatcher = scope.ServiceProvider.GetRequiredService<IMediator>();
        return await dispatcher.Send(query);
    }
}