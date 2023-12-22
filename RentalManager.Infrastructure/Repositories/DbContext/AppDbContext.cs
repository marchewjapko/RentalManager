using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;

namespace RentalManager.Infrastructure.Repositories.DbContext;

public class AppDbContext
    (DbContextOptions<AppDbContext> options) : IdentityDbContext<User, IdentityRole<int>, int>(
        options)
{
    public DbSet<Client> Clients { get; set; } = null!;

    public DbSet<Payment> Payments { get; set; } = null!;

    public DbSet<Agreement> Agreements { get; set; } = null!;

    public DbSet<Equipment> Equipment { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureIdentityDbContext();

        modelBuilder.ConfigureDomainEntities();
    }
}