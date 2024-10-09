using EFCore.BulkExtensions;
using TerrytLookup.Infrastructure.Repositories.DbContext;

namespace TerrytLookup.Infrastructure.Extensions;

public static class ContextExtensions
{
    public static Task InsertRelational<TEntity>(this AppDbContext context, IEnumerable<TEntity> entities) where TEntity : class
    {
        using var transaction = context.Database.BeginTransaction();
        context.BulkInsert(entities, new BulkConfig { SetOutputIdentity = true });

        return transaction.CommitAsync();
    }

    public static Task InsertNonRelational<TEntity>(this AppDbContext context, IEnumerable<TEntity> entities) where TEntity : class
    {
        context.AddRange(entities);

        return context.SaveChangesAsync();
    }
}