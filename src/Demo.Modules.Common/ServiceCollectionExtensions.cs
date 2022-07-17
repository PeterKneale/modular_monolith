using Demo.Modules.Common.Application.Behaviours;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Modules.Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRequestPreProcessor<>), typeof(LoggingBehaviour<>));
        services.AddTransient(typeof(IRequestPreProcessor<>), typeof(ValidationBehaviour<>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionalBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        return services;
    }
}