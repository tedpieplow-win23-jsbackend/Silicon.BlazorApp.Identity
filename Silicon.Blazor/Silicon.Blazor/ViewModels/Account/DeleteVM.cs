using System.ComponentModel.DataAnnotations;

namespace Silicon.Blazor.ViewModels.Account
{
    public class DeleteVM
    {
        [Display(Name = "Delete")]
        //[CheckboxRequired(ErrorMessage = "You must confirm if you want to delete")]
        public bool DeleteAccount { get; set; } = false;
    }
}
