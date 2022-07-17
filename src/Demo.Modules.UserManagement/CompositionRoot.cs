namespace Demo.Modules.UserManagement;

public static class CompositionRoot
{
    private static IServiceProvider _provider;

    public static void SetProvider(IServiceProvider provider)
    {
        _provider = provider;
    }

    public static IServiceScope BeginLifetimeScope()
    {
        return _provider.CreateScope();
    }
}