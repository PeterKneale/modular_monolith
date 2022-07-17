using System.Reflection;
using Demo.Modules.Common.Application.Contracts;
using Demo.Modules.UserManagement.Application.Contracts;
using Demo.Modules.UserManagement.Infrastructure.Persistence;
using Demo.Modules.UserManagement.Infrastructure.Repository;
using FluentMigrator.Runner;
using Npgsql;
using Polly;

namespace Demo.Modules.UserManagement;

public static class UserManagementStartup
{
    public static void Start(IConfiguration configuration, bool resetDb = false)
    {
        var serviceProvider = new ServiceCollection()
            .AddApplication()
            .AddInfrastructure(configuration)
            .BuildServiceProvider()
            .ApplyDatabaseMigrations(resetDb);
        
        CompositionRoot.SetProvider(serviceProvider);
    }
    
    private static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(assembly);
        services.AddValidatorsFromAssembly(assembly);
        return services;
    }

    private static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Db");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new Exception("Connection string missing");
        }

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddDbContext<DatabaseContext>(options => {
            options.UseNpgsql(connectionString);
            options.EnableDetailedErrors(true);
            options.EnableSensitiveDataLogging(true);
            options.UseSnakeCaseNamingConvention();
        });

        DefaultTypeMap.MatchNamesWithUnderscores = true;
        services.AddTransient<IDbConnection>(_ => new NpgsqlConnection(connectionString));

        services
            .AddFluentMigratorCore()
            .ConfigureRunner(runner => runner
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());

        return services;
    }

    private static IServiceProvider ApplyDatabaseMigrations(this IServiceProvider app, bool reset)
    {
        var RetryInterval = 5; // seconds
        var RetryAttempts = 1;
        var logs = app.GetRequiredService<ILogger<IMigrationRunner>>();

        var policy = Policy
            .Handle<Exception>()
            .WaitAndRetry(
                RetryAttempts,
                retryAttempt => TimeSpan.FromSeconds(RetryInterval),
                (exception, timeSpan, attempt, context) =>
                    logs.LogWarning($"Attempt {attempt} of {RetryAttempts} failed with exception {exception.Message}. Delaying {timeSpan.TotalMilliseconds}ms"));

        policy.Execute(() =>
        {
            using var scope = app.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            if (reset)
            {
                runner.MigrateDown(0);
            }
            runner.MigrateUp();
        });

        return app;
    }
}