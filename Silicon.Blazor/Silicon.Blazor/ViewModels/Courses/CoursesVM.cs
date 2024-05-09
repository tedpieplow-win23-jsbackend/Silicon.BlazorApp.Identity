namespace Silicon.Blazor.ViewModels.Courses;

public class CoursesVM
{
    public IEnumerable<CategoryModel>? Categories { get; set; }
    public IEnumerable<Course>? AllCourses { get; set; }
    public Pagination? Pagination { get; set; }
    public string? ErrorMessage { get; set; }
}
