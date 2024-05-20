using Microsoft.AspNetCore.Identity;
﻿using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Silicon.Blazor.Data;
using Silicon.Blazor.Factories;
using Silicon.Blazor.Models;
using System.Net;
using System.Text;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Silicon.Blazor.Services;

public class UserService(HttpClient httpClient, IConfiguration configuration, UserManager<ApplicationUser> userManager, AuthenticationStateProvider stateProvider, IServiceScopeFactory scopeFactory, ApplicationDbContext context)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = configuration;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly AuthenticationStateProvider _stateProvider = stateProvider;
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    private readonly ApplicationDbContext _context = context;

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
                var result = await httpResponse.Content.ReadAsStringAsync();
                var subscriberStatus = JsonConvert.DeserializeObject<SubscriberModel>(result);
                return ResponseFactory.Ok(subscriberStatus!.IsSubscribed);
            }
            else if (httpResponse.StatusCode == HttpStatusCode.Conflict)
            {
                var result = await httpResponse.Content.ReadAsStringAsync();
                var subscriberStatus = JsonConvert.DeserializeObject<SubscriberModel>(result);
                return ResponseFactory.Exists(subscriberStatus!.IsSubscribed);
            }
            else
                return ResponseFactory.Error();

        }
        catch (Exception ex)
        {
            return ResponseFactory.Error($"ERROR: {ex.Message}");
        }
    }

    public async Task<ApplicationUser> GetUserAsync()
    {
        var scope = _scopeFactory.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var userclaims = await GetUserClaimsAsync();
        var user = await userManager.GetUserAsync(userclaims);

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

    public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
    {
        var existingUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser != null)
        {
            _context.Entry(existingUser).State = EntityState.Detached;
        }

        _context.Users.Attach(user);
        _context.Entry(user).State = EntityState.Modified;

        return await _userManager.UpdateAsync(user);
    }
    public async Task<IdentityResult> UpdatePassUserAsync(ApplicationUser user, string oldPass, string newPass)
    {
        var existingUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser != null)
        {
            _context.Entry(existingUser).State = EntityState.Detached;
        }

        _context.Users.Attach(user);
        _context.Entry(user).State = EntityState.Modified;

        return await _userManager.ChangePasswordAsync(user, oldPass, newPass);
    }
}
