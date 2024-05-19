using Newtonsoft.Json;
using System.Text;

namespace Silicon.Blazor.Services;

public class SubscriptionService(IConfiguration configuration, HttpClient httpClient)
{
    private readonly IConfiguration _configuration = configuration;
    private readonly HttpClient _httpClient = httpClient;

    public async Task<bool> CheckSubscription(string email)
    {
        var apiUrl = _configuration.GetValue<string>("ConnectionStrings:GetSubscribersProvider");
        var requestData = new { Email = email };
        var jsonContent = JsonConvert.SerializeObject(requestData);
        var content = new StringContent(jsonContent, Encoding.UTF8, "appliction/json");
        var httpResponse = await _httpClient.PostAsync(apiUrl, content);

        if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var result = await httpResponse.Content.ReadAsStringAsync();
            var subscriberStatus = JsonConvert.DeserializeObject<SubscriberModel>(result);

            if (subscriberStatus != null)
            {
                return subscriberStatus.IsSubscribed;
            }
        }
        return false;
    }
}

public class SubscriberModel
{
    public string Email { get; set; } = null!;
    public bool IsSubscribed { get; set; }
    public bool DailyNewsLetter { get; set; }
    public bool AdvertisingUpdates { get; set; }
    public bool WeekInReviews { get; set; }
    public bool EventUpdates { get; set; }
    public bool StartupsWeekly { get; set; }
    public bool Podcasts { get; set; }
}
