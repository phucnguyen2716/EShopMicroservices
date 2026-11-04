using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace Ordering.API.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    public static void UpdateEntities(DbContext? context)
    {
        if(context==null) return;
        
        foreach(var entity in context.ChangeTracker.Entries<IEntity>())
        {
            if(entity.State == EntityState.Added)
            {
                entity.Entity.CreatedBy = "mehmet";
                entity.Entity.CreatedAt = DateTime.Now;
            }
            if(entity.State == EntityState.Added || entity.State == EntityState.Modified)
            {
                entity.Entity.LastModifiedBy = "mehmet";
                entity.Entity.LastModified = DateTime.Now;
            }
        }
    }
}
public static class Extensions
{
    public static bool HasChangedOwnEntities(this EntityEntry entry) =>
        entry.References.Any(r => 
        r.TargetEntry != null && 
        r.TargetEntry.Metadata.IsOwned() && 
        (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}