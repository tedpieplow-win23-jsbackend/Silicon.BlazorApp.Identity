using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Silicon.Blazor.Data;

public class ApplicationUser : IdentityUser
{
    [Required]
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;

    [Required]
    [ProtectedPersonalData]
    public string LastName { get; set; } = null!;

    [ProtectedPersonalData]
    public string? Biography { get; set; }
    public string? ProfileImageUrl { get; set; } = "profile-avatar.jpeg";
    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }
    public bool IsExternalAccount { get; set; } = false;
    public bool IsDarkMode { get; set; } = false;
    public bool IsSubscribed { get; set; } = false;


    public string? AddressId { get; set; } = null!;
    public AddressEntity? Address { get; set; } = null!;
}
