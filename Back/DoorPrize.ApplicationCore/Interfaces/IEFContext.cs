using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace DoorPrize.ApplicationCore.Interfaces
{
    public interface IEFContext
    {
        ChangeTracker ChangeTracker { get; }
        DatabaseFacade Database { get; }
        DbSet<TEntity> Set<TEntity>([NotNullAttribute] string name) where TEntity : class;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry Entry([NotNullAttribute] object entity);
        EntityEntry<TEntity> Entry<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}