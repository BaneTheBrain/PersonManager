using System.Linq.Expressions;

namespace PersonManagerService.Domain.Abstractions;

public interface IBaseRepository<T> where T : class
{
    T GetById(Guid id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);
}
