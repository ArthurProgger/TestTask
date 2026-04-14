using Application.DTOs;
using Application.Services;
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

    public void Create(OrderDto dto) => Execute(
        () => inner.Create(dto),
        $"Create: создание заказа на сумму {dto.Sum}",
        $"Create: заказ создан (Id={dto.Id}, Sum={dto.Sum})",
        $"Create: ошибка при создании заказа на сумму {dto.Sum}");

    public void Update(OrderDto dto) => Execute(
        () => inner.Update(dto),
        $"Update: обновление заказа Id={dto.Id}",
        $"Update: заказ Id={dto.Id} обновлён",
        $"Update: ошибка при обновлении заказа Id={dto.Id}");

    public void Delete(int orderId) => Execute(
        () => inner.Delete(orderId),
        $"Delete: удаление заказа Id={orderId}",
        $"Delete: заказ Id={orderId} удалён",
        $"Delete: ошибка при удалении заказа Id={orderId}");
}
