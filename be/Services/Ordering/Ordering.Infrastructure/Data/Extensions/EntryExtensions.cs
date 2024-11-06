using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ordering.Infrastructure.Data.Extensions;

public static class EntryExtensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) => 
        entry.References.Any(r => 
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            r.TargetEntry.State is EntityState.Added or EntityState.Modified);
}