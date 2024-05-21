using Microsoft.EntityFrameworkCore;
using Silicon.Blazor.Data;

namespace Silicon.Blazor.Factories;

public interface IDbContextFactory
{
    ApplicationDbContext CreateDbContext();
}

public class DbContextFactory(DbContextOptions<ApplicationDbContext> options) : IDbContextFactory
{
    private readonly DbContextOptions<ApplicationDbContext> _options = options;

    public ApplicationDbContext CreateDbContext()
    {
        return new ApplicationDbContext(_options);
    }
}
