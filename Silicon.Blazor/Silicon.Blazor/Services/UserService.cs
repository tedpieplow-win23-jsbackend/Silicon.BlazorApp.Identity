using Silicon.Blazor.Data;

namespace Silicon.Blazor.Services;

public class UserService(ApplicationDbContext context)
{
private readonly ApplicationDbContext _context = context;
}
