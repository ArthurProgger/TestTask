using Application.DTOs;
using Application.Services;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Decorators;

public class LoggingStafferService(
    IStafferService inner,
    ILogger<LoggingStafferService> logger,
    IDialogService dialogService) : LoggingServiceBase(logger, dialogService), IStafferService
{
    public IList<StafferDto> GetAll() => Execute(
        () => inner.GetAll(), [],
        "GetAll: запрос всех сотрудников",
        "GetAll: сотрудники получены",
        "GetAll: ошибка при получении сотрудников");

    public void Create(StafferDto dto) => Execute(
        () => inner.Create(dto),
        $"Create: создание сотрудника \"{dto.FullName}\"",
        $"Create: сотрудник \"{dto.FullName}\" создан (Id={dto.Id})",
        $"Create: ошибка при создании сотрудника \"{dto.FullName}\"");

    public void Update(StafferDto dto) => Execute(
        () => inner.Update(dto),
        $"Update: обновление сотрудника Id={dto.Id}",
        $"Update: сотрудник Id={dto.Id} обновлён",
        $"Update: ошибка при обновлении сотрудника Id={dto.Id}");

    public bool CanDelete(int stafferId) => Execute(
        () => inner.CanDelete(stafferId), false,
        $"CanDelete: проверка удаления сотрудника Id={stafferId}",
        $"CanDelete: проверка завершена для Id={stafferId}",
        $"CanDelete: ошибка при проверке удаления сотрудника Id={stafferId}");

    public void Delete(int stafferId) => Execute(
        () => inner.Delete(stafferId),
        $"Delete: удаление сотрудника Id={stafferId}",
        $"Delete: сотрудник Id={stafferId} удалён",
        $"Delete: ошибка при удалении сотрудника Id={stafferId}");
}
