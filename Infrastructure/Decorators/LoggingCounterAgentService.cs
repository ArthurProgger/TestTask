using Application.DTOs;
using Application.Services;
using Domain;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Decorators;

public class LoggingCounterAgentService(
    ICounterAgentService inner,
    ILogger<LoggingCounterAgentService> logger,
    IDialogService dialogService) : LoggingServiceBase(logger, dialogService), ICounterAgentService
{
    public IList<CounterAgentDto> GetAll() => Execute(
        () => inner.GetAll(), [],
        "GetAll: запрос всех контрагентов",
        "GetAll: контрагенты получены",
        "GetAll: ошибка при получении контрагентов");

    public IList<StafferDto> GetStaffers() => Execute(
        () => inner.GetStaffers(), [],
        "GetStaffers: запрос списка сотрудников",
        "GetStaffers: сотрудники получены",
        "GetStaffers: ошибка при получении сотрудников");

    public void Create(CounterAgentModel model) => Execute(
        () => inner.Create(model),
        $"Create: создание контрагента \"{model.Name}\"",
        $"Create: контрагент \"{model.Name}\" создан (Id={model.Id})",
        $"Create: ошибка при создании контрагента \"{model.Name}\"");

    public void Update(CounterAgentModel model) => Execute(
        () => inner.Update(model),
        $"Update: обновление контрагента Id={model.Id}",
        $"Update: контрагент Id={model.Id} обновлён",
        $"Update: ошибка при обновлении контрагента Id={model.Id}");

    public bool CanDelete(int counterAgentId) => Execute(
        () => inner.CanDelete(counterAgentId), false,
        $"CanDelete: проверка удаления контрагента Id={counterAgentId}",
        $"CanDelete: проверка завершена для Id={counterAgentId}",
        $"CanDelete: ошибка при проверке удаления контрагента Id={counterAgentId}");

    public void Delete(int counterAgentId) => Execute(
        () => inner.Delete(counterAgentId),
        $"Delete: удаление контрагента Id={counterAgentId}",
        $"Delete: контрагент Id={counterAgentId} удалён",
        $"Delete: ошибка при удалении контрагента Id={counterAgentId}");
}
