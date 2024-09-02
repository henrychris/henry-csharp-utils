# Henry C# Utils

This is a collection of code I use in different C# projects.

## Usage

### UnitOfWork

```cs
public interface IPlaceholderRepository : IRepository<Placeholder>{}

public class PlaceholderRepository(DataContext context) : BaseRepository<Placeholder>(context), IPlaceholderRepository { }

public class UnitOfWork(DataContext context) : IUnitOfWork
{
    public IPlaceholderRepository Placeholders => new PlaceholderRepository(context);

    public int Complete()
    {
        return context.SaveChanges();
    }
    public async Task<int> CompleteAsync()
    {
        return await context.SaveChangesAsync();
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            context.Dispose();
        }
    }
}

public abstract class BaseRepository<TEntity>(DataContext context) : IRepository<TEntity>
        where TEntity : BaseEntity
{
        protected DataContext Context { get; } = context;

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
        }

        public virtual async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
        {
            return await Context.Set<TEntity>().AnyAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdOrDefaultAsync(string id, bool withTracking = true, CancellationToken cancellationToken = default)
        {
            return withTracking
                ? await Context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken)
                : await Context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public virtual void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
        }
}
```
