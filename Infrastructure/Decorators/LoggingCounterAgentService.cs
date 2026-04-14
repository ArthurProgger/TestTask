using Application.DTOs;
using Application.Services;
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

    public void Create(CounterAgentDto dto) => Execute(
        () => inner.Create(dto),
        $"Create: создание контрагента \"{dto.Name}\"",
        $"Create: контрагент \"{dto.Name}\" создан (Id={dto.Id})",
        $"Create: ошибка при создании контрагента \"{dto.Name}\"");

    public void Update(CounterAgentDto dto) => Execute(
        () => inner.Update(dto),
        $"Update: обновление контрагента Id={dto.Id}",
        $"Update: контрагент Id={dto.Id} обновлён",
        $"Update: ошибка при обновлении контрагента Id={dto.Id}");

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
