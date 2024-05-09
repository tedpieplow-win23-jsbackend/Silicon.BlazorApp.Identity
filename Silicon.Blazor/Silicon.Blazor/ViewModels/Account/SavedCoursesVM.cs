using Silicon.Blazor.ViewModels.Courses;

namespace Silicon.Blazor.ViewModels.Account;

public class SavedCoursesVM
{
    public string Title { get; set; } = "Saved Items";
    public Course Course { get; set; } = new Course();

}
