using Silicon.Blazor.Components.Pages;
using Silicon.Blazor.Data;
using Silicon.Blazor.Data.Entities;
using Silicon.Blazor.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Silicon.Blazor.Services;

public class CourseService(HttpClient http, ApplicationDbContext context, UserService userService, IConfiguration configuration, ApplicationDbContext applicationDbContext)
{
    private readonly HttpClient _http = http;
    private readonly ApplicationDbContext _context = context;
    private readonly UserService _userService = userService;
    private readonly IConfiguration _configuration = configuration;
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

    public async Task<List<CourseCard>> GetCoursesAsync()
    {
        var courses = new List<CourseCard>();
        var query = new GraphQLQuery { Query = "{ getCourses { id title isBestseller categories title imageHeaderUri imageUri likesInPercent likes hours authors { name } prices { currency price discount } } }" };
        var response = await _http.PostAsJsonAsync(_configuration.GetValue<string>("ConnectionStrings:GetCoursesProvider"), query);

        if (response.IsSuccessStatusCode)
        {
            try
            {
                var result = await response.Content.ReadFromJsonAsync<DynamicGraphQLResponse>();
                courses = result?.Data.GetProperty("getCourses").EnumerateArray()
                .Where(course => !string.IsNullOrEmpty(course.GetProperty("id").GetString()))
                .Select(course => new CourseCard
                {
                    Id = course.TryGetProperty("id", out var idValue) ? idValue.GetString() ?? "" : "",
                    Title = course.GetProperty("title").GetString(),
                    IsBestseller = course.GetProperty("isBestseller").GetBoolean(),
                    ImageHeaderUri = course.TryGetProperty("imageHeaderUri", out var imageUriValue) ? imageUriValue.GetString() ?? "" : "",
                    ImageUri = course.TryGetProperty("imageUri", out var imageUri) ? imageUri.GetString() ?? "" : "",
                    Author = course.TryGetProperty("authors", out var authors) && authors.ValueKind == JsonValueKind.Array && authors.GetArrayLength() > 0
                                ? authors[0].GetProperty("name").GetString() ?? ""
                                : "",
                    Price = $"{course.GetProperty("prices").GetProperty("currency").GetString()} {course.GetProperty("prices").GetProperty("price").GetDouble()}",
                    DiscountPrice = course.GetProperty("prices").GetProperty("discount").GetDecimal() > 0
                                                                                                  ? course.GetProperty("prices").GetProperty("price").GetDecimal() - course.GetProperty("prices").GetProperty("discount").GetDecimal()
                                                                                                  : 0m,
                    Hours = course.GetProperty("hours").GetString(),
                    LikesInPercent = course.GetProperty("likesInPercent").GetString(),
                    LikesInNumbers = course.GetProperty("likes").GetString(),
                    Categories = course.TryGetProperty("categories", out var categories) && categories.ValueKind == JsonValueKind.Array && categories.GetArrayLength() > 0
                                ? categories.EnumerateArray().Select(cat => cat.GetString()).ToList()!
                                : new List<string>()
                })
                  .ToList();

            }
            catch (Exception)
            {
                Debug.WriteLine("Something went wrong, please try again later.");
            }
        }
        return courses!;
    }

   
}
