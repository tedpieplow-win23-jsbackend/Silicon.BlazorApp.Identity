

namespace Silicon.Blazor.Models;

public class CourseCard
{
    public string Id { get; set; } = null!;
    public bool IsBestseller { get; set; }
    public string? ImageUri { get; set; }
    public string? ImageHeaderUri { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Price { get; set; }
    public decimal DiscountPrice { get; set; }
    public string? Hours { get; set; }
    public string? LikesInPercent { get; set; }
    public string? LikesInNumbers { get; set; }
    public List<string>? Categories { get; set; }
    public string? Currency {  get; set; }
    public bool IsSaved { get; set; }
    public string? BookmarkColor { get; set; }
}

