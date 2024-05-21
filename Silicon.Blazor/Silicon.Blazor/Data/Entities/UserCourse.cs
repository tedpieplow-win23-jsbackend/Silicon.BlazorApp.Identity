using System.ComponentModel.DataAnnotations;

namespace Silicon.Blazor.Data.Entities;

public class UserCourse
{
    [Key]
    public int UserCourseId {  get; set; }
    public string ApplicationUserId { get; set; } = null!;
    public string CourseId { get; set; } = null!;
    public ApplicationUser? ApplicationUser { get; set; }
}
