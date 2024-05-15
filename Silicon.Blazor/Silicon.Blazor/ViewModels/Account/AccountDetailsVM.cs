using Silicon.Blazor.Data;
using Silicon.Blazor.ViewModels.Courses;

namespace Silicon.Blazor.ViewModels.Account;

public class AccountDetailsVM
{
    public ApplicationUser User { get; set; } = new ApplicationUser();
    public AddressInfoVM AddressInfo { get; set; } = new AddressInfoVM();
    public ContactInfoVM ContactInfo { get; set; } = new ContactInfoVM();
    public NavigationVM Navigation { get; set; } = new NavigationVM();
    public IEnumerable<Course> CourseList { get; set; } = new List<Course>();
    public Course Course { get; set; } = new Course();
    public ChangePasswordVM ChangePassword { get; set; } = new ChangePasswordVM();
    public DeleteVM Delete { get; set; } = new DeleteVM();
    public ProfileVM Profile { get; set; } = new ProfileVM();
    public string SuccessMessage { get; set; } = "";
    public string ErrorMessage { get; set; } = "";
}
