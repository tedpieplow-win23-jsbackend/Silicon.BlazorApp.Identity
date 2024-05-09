using System.ComponentModel.DataAnnotations;

namespace Silicon.Blazor.ViewModels.Account
{
    public class ChangePasswordVM
    {
        [Display(Name = "Current Password", Prompt = "******", Order = 0)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; } = null!;

        [Display(Name = "New Password", Prompt = "******", Order = 1)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password not strong enough")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{8,}$", ErrorMessage = "Password not strong enough")]
        public string NewPassword { get; set; } = null!;

        [Display(Name = "Confirm New password", Prompt = "******", Order = 2)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm your new password")]
        [Compare(nameof(NewPassword), ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
