using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;

namespace RentalManager.Infrastructure.Repositories.DbContext;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }

    public DbSet<Payment> Payments { get; set; }

    public DbSet<Agreement> Agreements { get; set; }

    public DbSet<Equipment> Equipments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureDomainEntities();
    }
}