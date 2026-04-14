using Application.DTOs;
using Application.Repositories;
using Application.Services;

namespace Infrastructure.Services;

public class StafferService(Func<IUnitOfWork> uowFactory) : IStafferService
{
    public IList<StafferDto> GetAll()
    {
        using var uow = uowFactory();
        return uow.Staffers.GetAll().Select(s => s.ToDto()).ToList();
    }

    public void Create(StafferDto dto)
    {
        using var uow = uowFactory();
        var model = dto.ToModel();
        uow.Staffers.Save(model);
        uow.Commit();
        dto.Id = model.Id;
    }

    public void Update(StafferDto dto)
    {
        using var uow = uowFactory();
        var model = dto.ToModel();
        uow.Staffers.Update(model);
        uow.Commit();
    }

    public bool CanDelete(int stafferId)
    {
        using var uow = uowFactory();
        var hasOrders = uow.Orders.Any(o => o.Staffer.Id == stafferId);
        var hasAgents = uow.CounterAgents.Any(c => c.Staffer.Id == stafferId);
        return !hasOrders && !hasAgents;
    }

    public void Delete(int stafferId)
    {
        using var uow = uowFactory();
        var entity = uow.Staffers.GetById(stafferId);

        if (entity == null) return;

        uow.Staffers.Delete(entity);
        uow.Commit();
    }
}
