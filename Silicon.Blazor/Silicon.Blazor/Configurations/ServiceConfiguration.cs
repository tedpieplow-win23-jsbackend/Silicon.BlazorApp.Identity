using Blazored.LocalStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Silicon.Blazor.Components.Account;
using Silicon.Blazor.Data;
using Silicon.Blazor.Factories;
using Silicon.Blazor.Repositories;
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
        services.AddScoped<IdentityRedirectManager>();
        services.AddScoped<ICoursesVM, CoursesVM>();
        services.AddScoped<UserService>();
        services.AddScoped<ClaimsPrincipal>();
        services.AddScoped<UserFactory>();
        services.AddScoped<ServiceBusHandler>();
        services.AddScoped<DarkModeService>();
        services.AddScoped<AuthService>();
        services.AddScoped<AddressService>();
        services.AddScoped<AddressFactory>();
        services.AddScoped<AddressRepository>();
        services.AddScoped<SubscriptionService>();
        services.AddScoped<CookieEvents>();
        services.AddScoped<CourseService>();

        services.AddSignalR();
        services.AddBlazoredLocalStorage();

        services.AddHttpContextAccessor();

        services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        })
            .AddIdentityCookies();

        services.ConfigureApplicationCookie(options =>
        {
            options.EventsType = typeof(CookieEvents);
        });

        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
            options.EnableSensitiveDataLogging();
        });


        services.AddScoped<IDbContextFactory, DbContextFactory>();

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddRoleStore<RoleStore<IdentityRole, ApplicationDbContext>>()
            .AddDefaultTokenProviders();
    }
}
