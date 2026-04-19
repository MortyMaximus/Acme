using Acme.Logic;
using Acme.Logic.Interfaces;
using Acme.Repository;
using Acme.Repository.Models;
using Acme.Web.Components;
using Microsoft.EntityFrameworkCore;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDatabase(builder.Configuration);
        builder.Services.AddApplicationServices();

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddControllersWithViews();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddHttpClient("api", client =>
        {
            client.BaseAddress = new Uri(builder.Configuration["BaseUrl"]!);
        });

        var app = builder.Build();

        app.UseApplicationMiddleware();

        app.MapControllers();
        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}

/// <summary>
/// For registration middleware.
/// </summary>
public static class WebApplicationExtensions
{
    public static WebApplication UseApplicationMiddleware(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
        }

        app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        //app.UseAuthentication();
        //app.UseAuthorization();

        app.UseAntiforgery();

        return app;
    }
}

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AcmeContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        );

        return services;
    }
}

/// <summary>
/// For Registrating Dependency Injection.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IDrawLogic, DrawLogic>();
        services.AddScoped<ISerialNumberLogic, SerialNumberLogic>();
        services.AddScoped<IRepositoryFacade, RepositoryFacade>();

        return services;
    }
}