using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Silicon.Blazor.Data;
using Silicon.Blazor.Models;
using System.Security.Claims;

namespace Silicon.Blazor.Services;

public class UserService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, HttpClient httpClient)
{
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly HttpClient _httpClient = httpClient;

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
                var apiUrl = "https://subscriptionprovider-silicon.azurewebsites.net/api/ToggleSubscriptionFunction?code=eBz-AH-lVbIir3LGga3ky_zdNHUoYDUfcKnNGXc-ejH1AzFuw1UUhg==";
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
