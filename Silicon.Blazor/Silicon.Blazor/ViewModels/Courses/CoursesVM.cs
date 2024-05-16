using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace Silicon.Blazor.ViewModels.Courses;

public class CoursesVM(HttpClient httpClient, NavigationManager navigationManager, IConfiguration configuration, ILogger<CoursesVM> logger) : ICoursesVM
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly NavigationManager _navigationManager = navigationManager;
    private readonly IConfiguration _configuration = configuration;
    private readonly ILogger<CoursesVM> _logger = logger;

    public IEnumerable<CategoryModel>? Categories { get; set; }
    public IEnumerable<Course>? AllCourses { get; set; }
    public Pagination? Pagination { get; set; }
    public string? ErrorMessage { get; set; }
    private bool ConnectionFailed { get; set; }

    public async Task OnInitializedAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (content != null)
                {
                    AllCourses = JsonConvert.DeserializeObject<IEnumerable<Course>>(content);
                }
            }
            else
            {
                Console.WriteLine($"Failed to retreive courses, status:: {response.StatusCode}");
                ConnectionFailed = GetConnectionFailedStatus();
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"An unexpected error occured: {ex.Message}");
        }
    }

    public void GoToCourseDetails(int courseId)
    {
        try
        {
            _navigationManager.NavigateTo($"/course/details/{courseId}");
        }
        catch (Exception ex)
        {
            ConnectionFailed = GetConnectionFailedStatus();
            _logger.LogInformation($"An unexpected error occured: {ex.Message}");
        }
    }

    public async Task SaveCourse(int courseId)
    {
        try
        {
            // TODO Insert connecrtionString and queueName whenever AQzure works again.
            string connectionString = _configuration.GetConnectionString("ServiceBusConnection")!;
            string queueName = _configuration["QueueName"]!;

            await using ServiceBusClient client = new ServiceBusClient(connectionString);
            ServiceBusSender sender = client.CreateSender(queueName);
            ServiceBusMessage message = new(courseId.ToString());

            await sender.SendMessageAsync(message);
        }
        catch (Exception ex)
        {
            ConnectionFailed = GetConnectionFailedStatus();
            _logger.LogInformation($"An unexpected error occured: {ex.Message}");
        }
    }

    public bool GetConnectionFailedStatus()
    {
        ConnectionFailed = true;
        return ConnectionFailed;
    }
}

