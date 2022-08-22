using System.Linq.Expressions;

namespace SimpleApi.Model.Base;

public interface IReadRepository<TEntity, TIdentity> where TEntity : Entity<TIdentity>
{
    Task<TEntity> Get(TIdentity id);
    Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true);
    Task<IReadOnlyCollection<TEntity>> Filter(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true);
    Task<IReadOnlyCollection<TEntity>> All();
}

public interface IWriteRepository<TEntity, TIdentity> where TEntity : Entity<TIdentity>
{
    Task Create(TEntity entity);
    Task CreateRange(IEnumerable<TEntity> entities);
    Task Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    Task Delete(TEntity entity, bool removeFromDb = false);
    Task DeleteRange(IEnumerable<TEntity> entities, bool removeFromDb = true);
}

public interface IRepository<TEntity, TIdentity> : IReadRepository<TEntity, TIdentity>,
    IWriteRepository<TEntity, TIdentity>
    where TEntity : Entity<TIdentity>
{
}