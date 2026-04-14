using Application.DTOs;
using Application.Repositories;
using Application.Services;

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

    public void Create(OrderDto dto)
    {
        using var uow = uowFactory();
        var staffer = uow.Staffers.GetById(dto.StafferId)
            ?? throw new InvalidOperationException($"Staffer with Id={dto.StafferId} not found");
        var counterAgent = uow.CounterAgents.GetById(dto.CounterAgentId)
            ?? throw new InvalidOperationException($"CounterAgent with Id={dto.CounterAgentId} not found");
        var model = dto.ToModel(staffer, counterAgent);
        uow.Orders.Save(model);
        uow.Commit();
        dto.Id = model.Id;
    }

    public void Update(OrderDto dto)
    {
        using var uow = uowFactory();
        var staffer = uow.Staffers.GetById(dto.StafferId)
            ?? throw new InvalidOperationException($"Staffer with Id={dto.StafferId} not found");
        var counterAgent = uow.CounterAgents.GetById(dto.CounterAgentId)
            ?? throw new InvalidOperationException($"CounterAgent with Id={dto.CounterAgentId} not found");
        var model = dto.ToModel(staffer, counterAgent);
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
