using MartinCostello.Logging.XUnit;
using Microsoft.Extensions.Configuration;

namespace Demo.Modules.CourseManagement.IntegrationTests;

public class ModuleFixture : ITestOutputHelperAccessor
{
    public ModuleFixture()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("testsettings.json")
            .Build();

        CourseManagementStartup.Start(configuration, resetDb: true);
    }

    public ITestOutputHelper? OutputHelper { get; set; }
}