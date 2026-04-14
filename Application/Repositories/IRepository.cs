using System.Linq.Expressions;

namespace Application.Repositories;

public interface IRepository<T> where T : class
{
    T? GetById(int id);
    IList<T> GetAll();
    bool Any(Expression<Func<T, bool>> predicate);
    void Save(T entity);
    void Update(T entity);
    void Delete(T entity);
}
