using Newtonsoft.Json;
using Silicon.Blazor.Data;
using Silicon.Blazor.Models;

namespace Silicon.Blazor.Factories;

public class UserFactory
{
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
        var result = await http.PostAsJsonAsync("https://subscriptionprovider-silicon.azurewebsites.net/api/GetSubscribersFunction?code=VdUem1ardDpuXjfbHNodWR6NRuTneq6ZTFo3n_8r7fHZAzFutbdzXA==", json);

        if (result.IsSuccessStatusCode)
            return true;

        return false;
    }
}
