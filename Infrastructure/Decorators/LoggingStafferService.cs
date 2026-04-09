using Application.DTOs;
using Application.Services;
using Domain;
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

    public void Create(StafferModel model) => Execute(
        () => inner.Create(model),
        $"Create: создание сотрудника \"{model.FullName}\"",
        $"Create: сотрудник \"{model.FullName}\" создан (Id={model.Id})",
        $"Create: ошибка при создании сотрудника \"{model.FullName}\"");

    public void Update(StafferModel model) => Execute(
        () => inner.Update(model),
        $"Update: обновление сотрудника Id={model.Id}",
        $"Update: сотрудник Id={model.Id} обновлён",
        $"Update: ошибка при обновлении сотрудника Id={model.Id}");

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
