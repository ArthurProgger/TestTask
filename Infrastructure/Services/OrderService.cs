using Application.Repositories;
using Application.Services;
using Domain;

namespace Infrastructure.Services;

public class OrderService(Func<IUnitOfWork> uowFactory) : IOrderService
{
    public IList<OrderModel> GetAll()
    {
        using var uow = uowFactory();
        return uow.Orders.GetAll();
    }

    public IList<StafferModel> GetStaffers()
    {
        using var uow = uowFactory();
        return uow.Staffers.GetAll();
    }

    public IList<CounterAgentModel> GetCounterAgents()
    {
        using var uow = uowFactory();
        return uow.CounterAgents.GetAll();
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

    public void Delete(OrderModel model)
    {
        using var uow = uowFactory();
        uow.Orders.Delete(model);
        uow.Commit();
    }
}
