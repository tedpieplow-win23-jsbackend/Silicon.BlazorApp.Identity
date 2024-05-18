using Silicon.Blazor.Data;

namespace Silicon.Blazor.Repositories;

public class AddressRepository(ApplicationDbContext context) : BaseRepo<AddressEntity>(context)
{
    private readonly ApplicationDbContext _context = context;
}
