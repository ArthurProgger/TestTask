using Application.DTOs;
using Domain;

namespace Application.Services;

public interface IOrderService
{
    IList<OrderDto> GetAll();
    IList<StafferDto> GetStaffers();
    IList<CounterAgentDto> GetCounterAgents();
    void Create(OrderModel model);
    void Update(OrderModel model);
    void Delete(int orderId);
}
