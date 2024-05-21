using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Silicon.Blazor.Data.Entities;

namespace Silicon.Blazor.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }
}
