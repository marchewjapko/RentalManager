using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;

namespace RentalManager.Infrastructure.Repositories.DbContext;

public static class DbContextConfiguration
{
    public static void ConfigureIdentityDbContext(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .ToTable("Users");

        modelBuilder.Entity<IdentityUserToken<int>>()
            .ToTable("UserTokens");

        modelBuilder.Entity<IdentityUserRole<int>>()
            .ToTable("UserRoles");

        modelBuilder.Entity<IdentityUserLogin<int>>()
            .ToTable("UserLogins");

        modelBuilder.Entity<IdentityUserClaim<int>>()
            .ToTable("UserClaims");

        modelBuilder.Entity<IdentityRole<int>>()
            .ToTable("Roles")
            .HasData(new IdentityRole<int>
            {
                Id = 1, Name = "Administrator",
                NormalizedName = "ADMINISTRATOR".ToLower()
            });

        modelBuilder.Entity<IdentityRoleClaim<int>>()
            .ToTable("RoleClaims", "dbo");
    }

    public static void ConfigureOnDeleteActions(this ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
    }
}