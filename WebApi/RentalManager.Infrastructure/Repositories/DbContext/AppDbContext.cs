using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;

namespace RentalManager.Infrastructure.Repositories.DbContext;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; } = null!;

    public DbSet<Payment> Payments { get; set; } = null!;

    public DbSet<Agreement> Agreements { get; set; } = null!;

    public DbSet<Equipment> Equipment { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureDomainEntities();
    }
}