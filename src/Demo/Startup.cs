using Demo.Modules.Common;
using Demo.Modules.UserManagement;

namespace Demo;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        CourseManagementStartup.Start(_configuration);
        UserManagementStartup.Start(_configuration);
        
        services.AddRazorPages().AddRazorRuntimeCompilation();
        services.AddLogging();
        
        services.AddScoped<ICourseManagementModule, CourseManagementModule>();
        services.AddScoped<IUserManagementModule, UserManagementModule>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
    }
}