using System.ComponentModel.DataAnnotations;

namespace Silicon.Blazor.Models;

public class ChangePassModel
{
    [Required(ErrorMessage = "Required")]
    [DataType(DataType.Password)]
    [Display(Name = "Current password")]
    public string OldPassword { get; set; } = "";

    [Display(Name = "New Password", Prompt = "******")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Required")]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{8,}$", ErrorMessage = "Password not strong enough")]
    public string NewPassword { get; set; } = "";

    [DataType(DataType.Password)]
    [Display(Name = "Confirm new password")]
    [Compare("NewPassword", ErrorMessage = "Password does not match")]
    public string ConfirmPassword { get; set; } = "";
}
