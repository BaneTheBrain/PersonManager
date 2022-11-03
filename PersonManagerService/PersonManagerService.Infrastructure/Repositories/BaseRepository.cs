using Microsoft.EntityFrameworkCore;
using PersonManagerService.Domain.Abstractions;
using PersonManagerService.Infrastructure.Contexts;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PersonManagerService.Persistance.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly PersonManagerServiceDbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(PersonManagerServiceDbContext personManagerServiceDbContext)
    {
        _dbContext = personManagerServiceDbContext;
        _dbSet = _dbContext.Set<T>();
    }
    public virtual void Insert(T entity) => _dbSet.Add(entity);

    public virtual void Update(T entity) => _dbSet.Update(entity);

    public virtual void Delete(T entity) => _dbSet.Remove(entity);

    public virtual T GetById(Guid id) => _dbSet.Find(id);

    public virtual IEnumerable<T> GetAll() => _dbSet.ToList();

    public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            return orderBy(query).ToList();
        }
        else
        {
            return query.ToList();
        }
    }
}
