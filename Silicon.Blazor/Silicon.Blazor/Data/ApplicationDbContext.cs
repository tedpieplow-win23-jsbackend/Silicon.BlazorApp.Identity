using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Silicon.Blazor.Data.Entities;

namespace Silicon.Blazor.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<UserAddressEntity> UserAddresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAddressEntity>()
            .HasKey(ua => new { ua.UserId, ua.AddressId });

        modelBuilder.Entity<UserAddressEntity>()
            .HasOne(ua => ua.User)
            .WithMany(u => u.UserAddresses)
            .HasForeignKey(ua => ua.UserId);

        modelBuilder.Entity<UserAddressEntity>()
            .HasOne(ua => ua.Address)
            .WithMany(a => a.UserAddresses)
            .HasForeignKey(a => a.AddressId);

        base.OnModelCreating(modelBuilder);
    }
}
