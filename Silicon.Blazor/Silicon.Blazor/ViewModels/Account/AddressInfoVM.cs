using System.ComponentModel.DataAnnotations;

namespace Silicon.Blazor.ViewModels.Account
{
    public class AddressInfoVM
    {
        public string Id { get; set; } = "";

        [DataType(DataType.Text)]
        [Display(Name = "Address Line 1", Prompt = "Enter your address line ", Order = 0)]
        [Required(ErrorMessage = "Required")]
        public string AddressLine1 { get; set; } = "";

        public string? AddressLine2 { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Postal Code", Prompt = "Postal code", Order = 1)]
        public string PostalCode { get; set; } = "";

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Required")]
        [Display(Name = "City", Prompt = "Enter your city", Order = 2)]
        public string City { get; set; } = "";
    }
}
