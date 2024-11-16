using Microsoft.EntityFrameworkCore;
using TerrytLookup.Core.Domain;

namespace TerrytLookup.Infrastructure.Repositories.DbContext;

public class AppDbContext(DbContextOptions<AppDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public const int PageSize = 20;

    public DbSet<Voivodeship> Voivodeships { get; init; }
    
    public DbSet<County> Counties { get; init; }

    public DbSet<Street> Streets { get; init; }

    public DbSet<Town> Towns { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<County>()
            .HasKey(x => new
            {
                TerrytVoivodeshipId = x.VoivodeshipId, TerrytCountyId = x.CountyId
            });
        
        modelBuilder.Entity<Street>()
            .HasKey(x => new
            {
                x.TownId, TerrytNameId = x.NameId
            });
        
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entry in entries) entry.Entity.UpdateTimestamp = DateTimeOffset.UtcNow;

        return base.SaveChanges();
    }
}