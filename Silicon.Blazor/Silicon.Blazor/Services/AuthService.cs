using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

namespace Silicon.Blazor.Services;

public class AuthService(AuthenticationStateProvider authenticationStateProvider, NavigationManager navigationManager)
{
    private readonly AuthenticationStateProvider _authenticationStateProvider = authenticationStateProvider;
    private readonly NavigationManager _navigationManager = navigationManager;

    public async Task<bool> EnsureAuthenticatedAsync()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (!user.Identity!.IsAuthenticated)
        {
            _navigationManager.NavigateTo("/signin");
            return false;
        }

        return true;
    }
}
