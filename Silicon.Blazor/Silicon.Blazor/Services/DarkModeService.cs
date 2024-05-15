using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Silicon.Blazor.Data;
using System.Security.Claims;

namespace Silicon.Blazor.Services;

public class DarkModeService(IServiceProvider serviceProvider)
{

    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public event Action<bool>? OnDarkModeChanged;
    public async Task SaveDarkModeSetting(bool isDarkMode)
    {
        var (authState, user) = await GetAuthenticationAsync();

        if (user.Identity!.IsAuthenticated)
        {
            var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var applicationUser = await userManager.GetUserAsync(user);
            if (applicationUser != null)
            {
                applicationUser.IsDarkMode = isDarkMode;
                await userManager.UpdateAsync(applicationUser);

                OnDarkModeChanged?.Invoke(isDarkMode);
            }
        }
    }

    public async Task<bool> UpdateDarkModeButtonSwitch()
    {
        var (authState, user) = await GetAuthenticationAsync();

        if (user.Identity!.IsAuthenticated)
        {
            var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var applicationUser = await userManager.GetUserAsync(user);
            if (applicationUser != null)
            {
                return applicationUser.IsDarkMode;
            }
        }
        return false;
    }

    private async Task<(AuthenticationState, ClaimsPrincipal)> GetAuthenticationAsync()
    {
        var authenticationStateProvider = _serviceProvider.GetRequiredService<AuthenticationStateProvider>();
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        return (authState,  user);
    }
}

