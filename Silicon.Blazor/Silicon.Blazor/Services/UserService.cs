using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Silicon.Blazor.Data;
using Silicon.Blazor.Factories;
using Silicon.Blazor.Models;
using System.Security.Claims;

namespace Silicon.Blazor.Services;

public class UserService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, HttpClient httpClient, IConfiguration configuration, ClaimsPrincipal userClaims, ILogger<UserService> logger)
{
    private readonly ApplicationDbContext _context = context;
    private readonly ClaimsPrincipal? _userClaims = userClaims;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = configuration;
    private readonly ILogger<UserService> _logger = logger;

    public async Task<ResponseResult> ManageSubscription(bool isSubscribed, string email)
    {
        try
        {
            //var loggedInUser = await _userManager.GetUserAsync(_userClaims!);
            //if (loggedInUser != null)
            //{
            var apiUrl = _configuration.GetValue<string>("ConnectionStrings:ToggleSubscription");
            var requestData = new { Email = email, IsSubscribed = isSubscribed };
            var jsonContent = JsonConvert.SerializeObject(requestData);
            var httpResponse = await _httpClient.PostAsJsonAsync(apiUrl, jsonContent);

            if (httpResponse.IsSuccessStatusCode)
            {
                //loggedInUser.IsSubscribed = isSubscribed;
                //var userId = loggedInUser.Id;
                //var response = await _context.SaveChangesAsync();
                return ResponseFactory.Ok();
            }
            else
                return ResponseFactory.Error("API-request failed.");
            //}
            //else
            //{
            //    return ResponseFactory.NotFound("User email is null.");
            //}
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error($"ERROR: {ex.Message}");
        }
    }
}
