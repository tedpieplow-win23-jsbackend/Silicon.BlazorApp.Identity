using Microsoft.AspNetCore.Identity;
﻿using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Silicon.Blazor.Data;
using Silicon.Blazor.Factories;
using Silicon.Blazor.Models;
using System.Net;
using System.Text;
using System.Security.Claims;

namespace Silicon.Blazor.Services;

public class UserService(HttpClient httpClient, IConfiguration configuration, UserManager<ApplicationUser> userManager, AuthenticationStateProvider stateProvider)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = configuration;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly AuthenticationStateProvider _stateProvider = stateProvider;

    public async Task<ResponseResult> ManageSubscription(bool isSubscribed, string email)
    {
        try
        {
            var apiUrl = _configuration.GetValue<string>("ConnectionStrings:ToggleSubscription");
            var requestData = new { Email = email, IsSubscribed = isSubscribed };
            var jsonContent = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "appliction/json");
            var httpResponse = await _httpClient.PostAsync(apiUrl, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                return ResponseFactory.Ok();
            }
            else if (httpResponse.StatusCode == HttpStatusCode.Conflict)
            {
                return ResponseFactory.Exists();
            }
            else
                return ResponseFactory.Error();

        }
        catch (Exception ex)
        {
            return ResponseFactory.Error($"ERROR: {ex.Message}");
        }
    }

    public async Task<ApplicationUser> GetUserAsync(HttpContext context)
    {
        await Task.Delay(20);
        var user = await _userManager.GetUserAsync(context.User);
        await Task.Delay(20);

        if (user != null)
            return user;

        return null!;
    }

    public async Task<ClaimsPrincipal> GetUserClaimsAsync()
    {
        var authenticationState = await _stateProvider.GetAuthenticationStateAsync();
        var user = authenticationState.User;

        if (user is not null)
        {
            return user;
        }

        return null!;
    }
}
