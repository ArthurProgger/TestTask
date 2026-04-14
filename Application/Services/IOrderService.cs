using Application.DTOs;

namespace Application.Services;

public interface IOrderService
{
    IList<OrderDto> GetAll();
    IList<StafferDto> GetStaffers();
    IList<CounterAgentDto> GetCounterAgents();
    void Create(OrderDto dto);
    void Update(OrderDto dto);
    void Delete(int orderId);
}
