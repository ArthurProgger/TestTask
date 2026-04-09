using Domain;

namespace Application.Services;

public interface IOrderService
{
    IList<OrderModel> GetAll();
    IList<StafferModel> GetStaffers();
    IList<CounterAgentModel> GetCounterAgents();
    void Create(OrderModel model);
    void Update(OrderModel model);
    void Delete(OrderModel model);
}
