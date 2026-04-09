namespace Application.Repositories;

public interface IUnitOfWork : IDisposable
{
    IStafferRepository Staffers { get; }
    ICounterAgentRepository CounterAgents { get; }
    IOrderRepository Orders { get; }
    void Commit();
    void Rollback();
}
