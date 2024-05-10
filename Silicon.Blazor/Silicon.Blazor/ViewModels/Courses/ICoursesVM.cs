
namespace Silicon.Blazor.ViewModels.Courses
{
    public interface ICoursesVM
    {
        IEnumerable<Course>? AllCourses { get; set; }
        IEnumerable<CategoryModel>? Categories { get; set; }
        string? ErrorMessage { get; set; }
        Pagination? Pagination { get; set; }

        bool GetConnectionFailedStatus();
        void GoToCourseDetails(int courseId);
        Task OnInitializedAsync();
        Task SaveCourse(int courseId);
    }
}