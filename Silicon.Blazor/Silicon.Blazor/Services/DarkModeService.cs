using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Silicon.Blazor.Data;
using System.Security.Claims;

namespace Silicon.Blazor.Services;

public class DarkModeService(AuthenticationStateProvider stateProvider, UserManager<ApplicationUser> userManager, IServiceProvider serviceProvider)
{
    private readonly AuthenticationStateProvider _stateProvider = stateProvider;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public event Action<bool>? OnDarkModeChanged;
    public async Task SaveDarkModeSetting(bool isDarkMode)
    {
        var authenticationStateProvider = _serviceProvider.GetRequiredService<AuthenticationStateProvider>();
        var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity!.IsAuthenticated)
        {
            var applicationUser = await userManager.GetUserAsync(user);
            if (applicationUser != null)
            {
                applicationUser.IsDarkMode = isDarkMode;
                await userManager.UpdateAsync(applicationUser);

                OnDarkModeChanged?.Invoke(isDarkMode);
            }
        }
    }
}

