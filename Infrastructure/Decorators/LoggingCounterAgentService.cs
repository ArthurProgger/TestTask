using Application.DTOs;
using Application.Services;
using Domain;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Decorators;

public class LoggingCounterAgentService(
    ICounterAgentService inner,
    ILogger<LoggingCounterAgentService> logger,
    IDialogService dialogService) : ICounterAgentService
{
    public IList<CounterAgentDto> GetAll()
    {
        logger.LogInformation("GetAll: запрос всех контрагентов");
        try
        {
            var result = inner.GetAll();
            logger.LogInformation("GetAll: получено {Count} контрагентов", result.Count);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "GetAll: ошибка при получении контрагентов");
            dialogService.ShowWarning(ex.Message);
            return [];
        }
    }

    public IList<StafferDto> GetStaffers()
    {
        logger.LogInformation("GetStaffers: запрос списка сотрудников");
        try
        {
            var result = inner.GetStaffers();
            logger.LogInformation("GetStaffers: получено {Count} сотрудников", result.Count);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "GetStaffers: ошибка при получении сотрудников");
            dialogService.ShowWarning(ex.Message);
            return [];
        }
    }

    public void Create(CounterAgentModel model)
    {
        logger.LogInformation("Create: создание контрагента \"{Name}\"", model.Name);
        try
        {
            inner.Create(model);
            logger.LogInformation("Create: контрагент \"{Name}\" создан (Id={Id})", model.Name, model.Id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Create: ошибка при создании контрагента \"{Name}\"", model.Name);
            dialogService.ShowWarning(ex.Message);
        }
    }

    public void Update(CounterAgentModel model)
    {
        logger.LogInformation("Update: обновление контрагента Id={Id}", model.Id);
        try
        {
            inner.Update(model);
            logger.LogInformation("Update: контрагент Id={Id} обновлён", model.Id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Update: ошибка при обновлении контрагента Id={Id}", model.Id);
            dialogService.ShowWarning(ex.Message);
        }
    }

    public bool CanDelete(int counterAgentId)
    {
        logger.LogInformation("CanDelete: проверка удаления контрагента Id={CounterAgentId}", counterAgentId);
        try
        {
            var result = inner.CanDelete(counterAgentId);
            logger.LogInformation("CanDelete: контрагент Id={CounterAgentId}, результат={Result}", counterAgentId, result);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "CanDelete: ошибка при проверке удаления контрагента Id={CounterAgentId}", counterAgentId);
            dialogService.ShowWarning(ex.Message);
            return false;
        }
    }

    public void Delete(int counterAgentId)
    {
        logger.LogInformation("Delete: удаление контрагента Id={CounterAgentId}", counterAgentId);
        try
        {
            inner.Delete(counterAgentId);
            logger.LogInformation("Delete: контрагент Id={CounterAgentId} удалён", counterAgentId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Delete: ошибка при удалении контрагента Id={CounterAgentId}", counterAgentId);
            dialogService.ShowWarning(ex.Message);
        }
    }
}
