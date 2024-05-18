using Newtonsoft.Json;
using Silicon.Blazor.Data;
using Silicon.Blazor.Models;
using Silicon.Blazor.ViewModels.Account;
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

    public ApplicationUser PopulateUserEntity(ContactInfoVM model, ApplicationUser user)
    {
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.Email = model.Email;
        user.PhoneNumber = model.Phone;
        user.Biography = model.Bio;

        return user;
    }

    public ContactInfoVM PopulateContactInfo(ApplicationUser user)
    {
        return new ContactInfoVM
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            Phone = user.PhoneNumber!,
            Bio = user.Biography,
            IsExternalAccount = user.IsExternalAccount
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
