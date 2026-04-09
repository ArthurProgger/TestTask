using Application.Repositories;
using NHibernate;

namespace Infrastructure.Repositories;

public class NHibernateUnitOfWork : IUnitOfWork
{
    private readonly ISession _session;
    private readonly ITransaction _transaction;

    public IStafferRepository Staffers { get; }
    public ICounterAgentRepository CounterAgents { get; }
    public IOrderRepository Orders { get; }

    public NHibernateUnitOfWork()
    {
        _session = NHibernateHelper.аOpenSession();
        _transaction = _session.BeginTransaction();

        Staffers = new StafferRepository(_session);
        CounterAgents = new CounterAgentRepository(_session);
        Orders = new OrderRepository(_session);
    }

    public void Commit() => _transaction.Commit();

    public void Rollback() => _transaction.Rollback();

    public void Dispose()
    {
        _transaction.Dispose();
        _session.Dispose();
    }
}
