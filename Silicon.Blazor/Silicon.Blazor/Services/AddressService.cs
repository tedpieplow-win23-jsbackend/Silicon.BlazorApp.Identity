using Silicon.Blazor.Data;
using Silicon.Blazor.Factories;
using Silicon.Blazor.Models;
using Silicon.Blazor.Repositories;
using Silicon.Blazor.ViewModels.Account;
using System.Linq.Expressions;

namespace Silicon.Blazor.Services;

public class AddressService(AddressRepository addressRepository, AddressFactory addressFactory, UserService userService)
{
    private readonly AddressRepository _addressRepository = addressRepository;
    private readonly AddressFactory _addressFactory = addressFactory;
    private readonly UserService _userService = userService;
    public async Task<ResponseResult> GetOrCreateAddressAsync(AddressInfoVM model)
    {
        try
        {
            Expression<Func<AddressEntity, bool>> addressExpression = x => x.AddressLine1 == model.AddressLine1 && x.AddressLine2 == model.AddressLine2 && x.City == model.City && x.PostalCode == model.PostalCode;
            var existResult = await _addressRepository.ExistsAsync(addressExpression);

            if (existResult.StatusCode == StatusCode.EXISTS)
            {
                var getResult = await _addressRepository.GetOneAsync(addressExpression);
                if (getResult.StatusCode == StatusCode.OK)
                    return ResponseFactory.Ok(getResult.ContentResult!);
            }
            else if (existResult.StatusCode == StatusCode.NOT_FOUND)
            {
                var newAddressEntity = _addressFactory.PopulateAddressEntity(model);
                var createResult = await _addressRepository.CreateAsync(newAddressEntity);
                if (createResult.StatusCode == StatusCode.OK)
                    return ResponseFactory.Ok(createResult.ContentResult!);
            }

            return ResponseFactory.Error("Something went wrong with creating or getting the address.");
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> UpdateUserWithAddress(ApplicationUser user, AddressInfoVM model)
    {
        try
        {
            var responseResult = await GetOrCreateAddressAsync(model);
            if (responseResult.StatusCode == StatusCode.OK)
            {
                var addressEntity = (AddressEntity)responseResult.ContentResult!;
                user.AddressId = addressEntity.Id;

                var updateResult = await _userService.UpdateUserAsync(user);
                if (updateResult.Succeeded)
                    return ResponseFactory.Ok("Updated successfully.");
            }

            return ResponseFactory.Error("Something went wrong.");
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message); ;
        }
    }
}
