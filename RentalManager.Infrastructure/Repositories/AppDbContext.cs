using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;

namespace RentalManager.Infrastructure.Repositories;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; } = null!;

    public DbSet<Employee> Employees { get; set; } = null!;

    public DbSet<Payment> Payments { get; set; } = null!;

    public DbSet<RentalAgreement> RentalAgreements { get; set; } = null!;

    public DbSet<RentalEquipment> RentalEquipment { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RentalEquipment>()
            .HasIndex(c => c.Id)
            .IsUnique();
    }
}