using Microsoft.EntityFrameworkCore;
using TerrytLookup.Core.Domain;

namespace TerrytLookup.Infrastructure.Repositories.DbContext;

public class AppDbContext(DbContextOptions<AppDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options), IDisposable
{
    public const int PageSize = 20;

    public DbSet<Voivodeship> Voivodeships { get; init; }

    public DbSet<Street> Streets { get; init; }

    public DbSet<Town> Towns { get; init; }
}