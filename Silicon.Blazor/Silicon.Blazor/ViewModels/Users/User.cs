using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Silicon.Blazor.ViewModels.Users;

public class User
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
    public List<int>? SavedCourseIds { get; set; } = new List<int>();

}
