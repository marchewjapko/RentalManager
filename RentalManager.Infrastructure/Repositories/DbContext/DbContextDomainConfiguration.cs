using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;

namespace RentalManager.Infrastructure.Repositories.DbContext;

public static class DbContextDomainConfiguration
{
    public static void ConfigureDomainEntities(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agreement>()
            .HasOne(e => e.Employee)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Agreement>()
            .HasOne(e => e.User)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);        
        
        modelBuilder.Entity<Equipment>()
            .HasOne(e => e.User)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Payment>()
            .HasOne(e => e.User)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Client>()
            .HasOne(e => e.User)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Agreement>()
            .HasMany(e => e.Equipment)
            .WithMany(e => e.Agreements);
        
        modelBuilder.Entity<Agreement>()
            .HasMany(t => t.Equipment)
            .WithMany(s => s.Agreements)
            .UsingEntity<Dictionary<string, object>>(
                x => x.HasOne<Equipment>().WithMany().OnDelete(DeleteBehavior.Restrict),
                x => x.HasOne<Agreement>().WithMany().OnDelete(DeleteBehavior.Cascade)
            );
    }
}