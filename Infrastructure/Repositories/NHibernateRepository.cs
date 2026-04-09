using Application.Repositories;
using NHibernate;

namespace Infrastructure.Repositories;

public class NHibernateRepository<T>(ISession session) : IRepository<T>
    where T : class
{
    public T? GetById(int id) => session.Get<T>(id);

    public IList<T> GetAll() => session.Query<T>().ToList();

    public void Save(T entity) => session.Save(entity);

    public void Update(T entity) => session.Update(entity);

    public void Delete(T entity) => session.Delete(entity);
}
