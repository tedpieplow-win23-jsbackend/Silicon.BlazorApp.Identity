using Newtonsoft.Json;
using Silicon.Blazor.Data;
using Silicon.Blazor.Models;
using System.Text;

namespace Silicon.Blazor.Factories;

public class UserFactory(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    public async Task<ApplicationUser> PopulateUserEntity(SignUpModel model)
    {
        return new ApplicationUser
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            UserName = model.Email,
            IsSubscribed = await CheckIfSubscribed(model.Email),
        };
    }

    public async Task<bool> CheckIfSubscribed(string email)
    {
        HttpClient http = new HttpClient();
        var json = JsonConvert.SerializeObject(new { Email = email });
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await http.PostAsync(_configuration.GetConnectionString("GetSubscribersProvider"), content);

        if (result.IsSuccessStatusCode)
            return true;

        return false;
    }
}
