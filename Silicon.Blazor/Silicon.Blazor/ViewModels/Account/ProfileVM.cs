namespace Silicon.Blazor.ViewModels.Account;

public class ProfileVM
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? ProfileImageUrl { get; set; }
    public string? AltText { get; set; }
    public NavigationVM NavigationViewModel { get; set; } = new NavigationVM();
}
