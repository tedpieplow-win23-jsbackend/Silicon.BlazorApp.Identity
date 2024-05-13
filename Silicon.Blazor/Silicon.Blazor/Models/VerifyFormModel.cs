using System.ComponentModel.DataAnnotations;

namespace Silicon.Blazor.Models;

public class VerifyFormModel
{
    [Required]
    public string Code { get; set; } = null!;
}
