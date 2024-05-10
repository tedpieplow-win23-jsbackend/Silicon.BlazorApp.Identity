using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Silicon.Blazor.Data;
using Silicon.Blazor.Models;
using System.Security.Claims;

namespace Silicon.Blazor.Services;

public class UserService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, HttpClient httpClient, IConfiguration configuration)
{
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = configuration;

    public async Task<ResponseResult> ManageSubscription(bool isSubscribed, ClaimsPrincipal userClaims)
    {
        try
        {
            var loggedInUser = await _userManager.GetUserAsync(userClaims);
            if (loggedInUser != null)
            {
                loggedInUser.IsSubscribed = isSubscribed;
                await _context.SaveChangesAsync();

                var email = loggedInUser?.Email;
                var apiUrl = _configuration.GetValue<string>("ConnectionStrings:ToggleSubscription");
                var requestData = new { email, isSubscribed };
                var jsonContent = JsonConvert.SerializeObject(requestData);
                var httpResponse = await _httpClient.PostAsJsonAsync(apiUrl, jsonContent);

                if (httpResponse.IsSuccessStatusCode)
                    return ResponseFactory.Ok();
                else
                    return ResponseFactory.Error("API-request failed.");
            }
            else
            {
                return ResponseFactory.NotFound("User email is null.");
            }
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error($"ERROR: {ex.Message}");
        }
    }
}
