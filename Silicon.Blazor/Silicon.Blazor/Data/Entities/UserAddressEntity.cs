using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Silicon.Blazor.Data.Entities;

public class UserAddressEntity
{
    [Key]
    [Column(Order = 1)]
    public string UserId { get; set; } = null!;
    [Key]
    [Column(Order = 2)]
    public string AddressId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
    public AddressEntity Address { get; set; } = null!;
}
