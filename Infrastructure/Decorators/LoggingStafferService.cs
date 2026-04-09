using Application.DTOs;
using Application.Services;
using Domain;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Decorators;

public class LoggingStafferService(
    IStafferService inner,
    ILogger<LoggingStafferService> logger,
    IDialogService dialogService) : IStafferService
{
    public IList<StafferDto> GetAll()
    {
        logger.LogInformation("GetAll: запрос всех сотрудников");
        try
        {
            var result = inner.GetAll();
            logger.LogInformation("GetAll: получено {Count} сотрудников", result.Count);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "GetAll: ошибка при получении сотрудников");
            dialogService.ShowWarning(ex.Message);
            return [];
        }
    }

    public void Create(StafferModel model)
    {
        logger.LogInformation("Create: создание сотрудника \"{FullName}\"", model.FullName);
        try
        {
            inner.Create(model);
            logger.LogInformation("Create: сотрудник \"{FullName}\" создан (Id={Id})", model.FullName, model.Id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Create: ошибка при создании сотрудника \"{FullName}\"", model.FullName);
            dialogService.ShowWarning(ex.Message);
        }
    }

    public void Update(StafferModel model)
    {
        logger.LogInformation("Update: обновление сотрудника Id={Id}", model.Id);
        try
        {
            inner.Update(model);
            logger.LogInformation("Update: сотрудник Id={Id} обновлён", model.Id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Update: ошибка при обновлении сотрудника Id={Id}", model.Id);
            dialogService.ShowWarning(ex.Message);
        }
    }

    public bool CanDelete(int stafferId)
    {
        logger.LogInformation("CanDelete: проверка удаления сотрудника Id={StafferId}", stafferId);
        try
        {
            var result = inner.CanDelete(stafferId);
            logger.LogInformation("CanDelete: сотрудник Id={StafferId}, результат={Result}", stafferId, result);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "CanDelete: ошибка при проверке удаления сотрудника Id={StafferId}", stafferId);
            dialogService.ShowWarning(ex.Message);
            return false;
        }
    }

    public void Delete(int stafferId)
    {
        logger.LogInformation("Delete: удаление сотрудника Id={StafferId}", stafferId);
        try
        {
            inner.Delete(stafferId);
            logger.LogInformation("Delete: сотрудник Id={StafferId} удалён", stafferId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Delete: ошибка при удалении сотрудника Id={StafferId}", stafferId);
            dialogService.ShowWarning(ex.Message);
        }
    }
}
