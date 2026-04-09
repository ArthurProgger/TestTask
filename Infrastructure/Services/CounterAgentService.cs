using Application.Repositories;
using Application.Services;
using Domain;

namespace Infrastructure.Services;

public class CounterAgentService(Func<IUnitOfWork> uowFactory) : ICounterAgentService
{
    public IList<CounterAgentModel> GetAll()
    {
        using var uow = uowFactory();
        return uow.CounterAgents.GetAll();
    }

    public IList<StafferModel> GetStaffers()
    {
        using var uow = uowFactory();
        return uow.Staffers.GetAll();
    }

    public void Create(CounterAgentModel model)
    {
        using var uow = uowFactory();
        uow.CounterAgents.Save(model);
        uow.Commit();
    }

    public void Update(CounterAgentModel model)
    {
        using var uow = uowFactory();
        uow.CounterAgents.Update(model);
        uow.Commit();
    }

    public bool CanDelete(int counterAgentId)
    {
        using var uow = uowFactory();
        var hasOrders = uow.Orders.GetAll().Any(o => o.CounterAgent?.Id == counterAgentId);
        return !hasOrders;
    }

    public void Delete(CounterAgentModel model)
    {
        using var uow = uowFactory();
        uow.CounterAgents.Delete(model);
        uow.Commit();
    }
}
