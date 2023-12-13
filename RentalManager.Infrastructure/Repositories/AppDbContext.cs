using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;

namespace RentalManager.Infrastructure.Repositories;

public class AppDbContext : IdentityDbContext<Employee, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; } = null!;

    public DbSet<Employee> Employees { get; set; } = null!;

    public DbSet<Payment> Payments { get; set; } = null!;

    public DbSet<Agreement> Agreements { get; set; } = null!;

    public DbSet<Equipment> Equipment { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Employee>()
            .Ignore(x => x.Email);
        modelBuilder.Entity<Employee>()
            .Ignore(x => x.NormalizedEmail);
        modelBuilder.Entity<Employee>()
            .Ignore(x => x.EmailConfirmed);
        modelBuilder.Entity<Employee>()
            .Ignore(x => x.PhoneNumber);
        modelBuilder.Entity<Employee>()
            .Ignore(x => x.PhoneNumberConfirmed);
        modelBuilder.Entity<Employee>()
            .Ignore(x => x.TwoFactorEnabled);
    }
}