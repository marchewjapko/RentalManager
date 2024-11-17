using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;

namespace RentalManager.Infrastructure.Repositories.DbContext;

public static class DbContextDomainConfiguration
{
    public static void ConfigureDomainEntities(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agreement>()
            .HasMany(e => e.Equipments)
            .WithMany(e => e.Agreements);

        modelBuilder.Entity<Agreement>()
            .HasMany(x => x.Equipments)
            .WithMany(x => x.Agreements)
            .UsingEntity<Dictionary<string, object>>(
                x => x.HasOne<Equipment>()
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict),
                x => x.HasOne<Agreement>()
                    .WithMany()
            );
    }
}