using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Silicon.Blazor.Components.Account;
using Silicon.Blazor.Data;
using Silicon.Blazor.Factories;
using Silicon.Blazor.Services;
using Silicon.Blazor.ViewModels.Courses;
using System.Security.Claims;

namespace Silicon.Blazor.Configurations;

public static class ServiceConfiguration
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRazorComponents()
              .AddInteractiveServerComponents()
              .AddInteractiveWebAssemblyComponents();

        services.AddHttpClient();
        services.AddCascadingAuthenticationState();
        services.AddScoped<IdentityUserAccessor>();
        services.AddScoped<IdentityRedirectManager>();
        services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();
        services.AddScoped<ICoursesVM, CoursesVM>();
        services.AddScoped<UserService>();
        services.AddScoped<ClaimsPrincipal>();
        services.AddScoped<UserFactory>();
        services.AddScoped<ServiceBusHandler>();
        services.AddHttpContextAccessor();

        services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();


        services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        })
            .AddIdentityCookies();

        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();
    }
}
