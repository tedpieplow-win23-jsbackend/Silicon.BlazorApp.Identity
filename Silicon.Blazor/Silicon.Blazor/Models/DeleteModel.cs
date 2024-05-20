using System.ComponentModel.DataAnnotations;

namespace Silicon.Blazor.Models;

public class DeleteModel
{
    [Required]
    public bool Delete { get; set; }
}
