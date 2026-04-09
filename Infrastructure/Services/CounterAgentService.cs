using Application.DTOs;
using Application.Repositories;
using Application.Services;
using Domain;

namespace Infrastructure.Services;

public class CounterAgentService(Func<IUnitOfWork> uowFactory) : ICounterAgentService
{
    public IList<CounterAgentDto> GetAll()
    {
        using var uow = uowFactory();
        return uow.CounterAgents.GetAll().Select(c => c.ToDto()).ToList();
    }

    public IList<StafferDto> GetStaffers()
    {
        using var uow = uowFactory();
        return uow.Staffers.GetAll().Select(s => s.ToDto()).ToList();
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
        var hasOrders = uow.Orders.GetAll().Any(o => o.CounterAgent.Id == counterAgentId);
        return !hasOrders;
    }

    public void Delete(int counterAgentId)
    {
        using var uow = uowFactory();
        var entity = uow.CounterAgents.GetById(counterAgentId);
        
        if (entity == null) return;
        
        uow.CounterAgents.Delete(entity);
        uow.Commit();
    }
}