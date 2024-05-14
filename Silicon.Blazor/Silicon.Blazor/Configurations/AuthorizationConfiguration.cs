namespace Silicon.Blazor.Configurations;

public static class AuthorizationConfiguration
{
    public static void RegisterAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("SuperUser", policy => policy.RequireRole("SuperUser"))
            .AddPolicy("Administrator", policy => policy.RequireRole("SuperUser", "Administrator"))
            .AddPolicy("Author", policy => policy.RequireRole("SuperUser", "Administrator", "Author"))
            .AddPolicy("AuthenticatedUsers", policy => policy.RequireRole("SuperUser", "Administrator", "Author", "User"));
    }
}
