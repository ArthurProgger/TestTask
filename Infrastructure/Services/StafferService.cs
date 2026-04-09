using Application.Repositories;
using Application.Services;
using Domain;

namespace Infrastructure.Services;

public class StafferService(Func<IUnitOfWork> uowFactory) : IStafferService
{
    public IList<StafferModel> GetAll()
    {
        using var uow = uowFactory();
        return uow.Staffers.GetAll();
    }

    public void Create(StafferModel model)
    {
        using var uow = uowFactory();
        uow.Staffers.Save(model);
        uow.Commit();
    }

    public void Update(StafferModel model)
    {
        using var uow = uowFactory();
        uow.Staffers.Update(model);
        uow.Commit();
    }

    public bool CanDelete(int stafferId)
    {
        using var uow = uowFactory();
        var hasOrders = uow.Orders.GetAll().Any(o => o.Staffer?.Id == stafferId);
        var hasAgents = uow.CounterAgents.GetAll().Any(c => c.Staffer?.Id == stafferId);
        return !hasOrders && !hasAgents;
    }

    public void Delete(StafferModel model)
    {
        using var uow = uowFactory();
        uow.Staffers.Delete(model);
        uow.Commit();
    }
}
