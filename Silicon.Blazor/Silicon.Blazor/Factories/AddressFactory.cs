using Silicon.Blazor.Data;
using Silicon.Blazor.Repositories;
using Silicon.Blazor.ViewModels.Account;

namespace Silicon.Blazor.Factories;

public class AddressFactory(AddressRepository addressRepository)
{
    private readonly AddressRepository _addressRepository = addressRepository;

    public AddressEntity PopulateAddressEntity(AddressInfoVM model)
    {
        return new AddressEntity
        {
            AddressLine1 = model.AddressLine1,
            AddressLine2 = model.AddressLine2,
            City = model.City,
            PostalCode = model.PostalCode,
        };
    }

    public async Task<AddressInfoVM> PopulateAddressForm(ApplicationUser user)
    {
        var result = await _addressRepository.GetOneAsync(x => x.Id == user.AddressId);
        var addressEntity = (AddressEntity)result.ContentResult!;
        var model = new AddressInfoVM();

        if (addressEntity != null)
        {
            model.AddressLine1 = addressEntity.AddressLine1;
            model.AddressLine2 = addressEntity.AddressLine2;
            model.PostalCode = addressEntity.PostalCode;
            model.City = addressEntity.City;
        }
        return model;
    }
}
