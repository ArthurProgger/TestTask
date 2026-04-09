using Application.DTOs;
using Application.Repositories;
using Application.Services;
using Domain;

namespace Infrastructure.Services;

public class OrderService(Func<IUnitOfWork> uowFactory) : IOrderService
{
    public IList<OrderDto> GetAll()
    {
        using var uow = uowFactory();
        return uow.Orders.GetAll().Select(o => o.ToDto()).ToList();
    }

    public IList<StafferDto> GetStaffers()
    {
        using var uow = uowFactory();
        return uow.Staffers.GetAll().Select(s => s.ToDto()).ToList();
    }

    public IList<CounterAgentDto> GetCounterAgents()
    {
        using var uow = uowFactory();
        return uow.CounterAgents.GetAll().Select(c => c.ToDto()).ToList();
    }

    public void Create(OrderModel model)
    {
        using var uow = uowFactory();
        uow.Orders.Save(model);
        uow.Commit();
    }

    public void Update(OrderModel model)
    {
        using var uow = uowFactory();
        uow.Orders.Update(model);
        uow.Commit();
    }

    public void Delete(int orderId)
    {
        using var uow = uowFactory();
        var entity = uow.Orders.GetById(orderId);
        
        if (entity == null) return;
        
        uow.Orders.Delete(entity);
        uow.Commit();
    }
}