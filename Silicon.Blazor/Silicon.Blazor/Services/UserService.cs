using Microsoft.AspNetCore.Identity;
using Silicon.Blazor.Data;
using Silicon.Blazor.Models;
using System.Security.Claims;

namespace Silicon.Blazor.Services;

public class UserService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
{
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<ResponseResult> ManageSubscription(bool isSubscribed, ClaimsPrincipal userClaims)
    {
        try
        {
            var loggedInUser = await _userManager.GetUserAsync(userClaims);
            if (loggedInUser != null)
            {
                loggedInUser.IsSubscribed = isSubscribed;
                await _context.SaveChangesAsync();
                return ResponseFactory.Ok();
            }
            else
            {
                return ResponseFactory.NotFound("User doesn't exists.");
            }
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error($"ERROR: {ex.Message}");
        }
    }
}
