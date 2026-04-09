using Application.DTOs;
using Application.Services;
using Domain;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Decorators;

public class LoggingOrderService(
    IOrderService inner,
    ILogger<LoggingOrderService> logger,
    IDialogService dialogService) : LoggingServiceBase(logger, dialogService), IOrderService
{
    public IList<OrderDto> GetAll() => Execute(
        () => inner.GetAll(), [],
        "GetAll: запрос всех заказов",
        "GetAll: заказы получены",
        "GetAll: ошибка при получении заказов");

    public IList<StafferDto> GetStaffers() => Execute(
        () => inner.GetStaffers(), [],
        "GetStaffers: запрос списка сотрудников",
        "GetStaffers: сотрудники получены",
        "GetStaffers: ошибка при получении сотрудников");

    public IList<CounterAgentDto> GetCounterAgents() => Execute(
        () => inner.GetCounterAgents(), [],
        "GetCounterAgents: запрос списка контрагентов",
        "GetCounterAgents: контрагенты получены",
        "GetCounterAgents: ошибка при получении контрагентов");

    public void Create(OrderModel model) => Execute(
        () => inner.Create(model),
        $"Create: создание заказа на сумму {model.Sum}",
        $"Create: заказ создан (Id={model.Id}, Sum={model.Sum})",
        $"Create: ошибка при создании заказа на сумму {model.Sum}");

    public void Update(OrderModel model) => Execute(
        () => inner.Update(model),
        $"Update: обновление заказа Id={model.Id}",
        $"Update: заказ Id={model.Id} обновлён",
        $"Update: ошибка при обновлении заказа Id={model.Id}");

    public void Delete(int orderId) => Execute(
        () => inner.Delete(orderId),
        $"Delete: удаление заказа Id={orderId}",
        $"Delete: заказ Id={orderId} удалён",
        $"Delete: ошибка при удалении заказа Id={orderId}");
}
