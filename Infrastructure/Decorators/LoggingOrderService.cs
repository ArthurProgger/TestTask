using Application.DTOs;
using Application.Services;
using Domain;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Decorators;

public class LoggingOrderService(
    IOrderService inner,
    ILogger<LoggingOrderService> logger,
    IDialogService dialogService) : IOrderService
{
    public IList<OrderDto> GetAll()
    {
        logger.LogInformation("GetAll: запрос всех заказов");
        try
        {
            var result = inner.GetAll();
            logger.LogInformation("GetAll: получено {Count} заказов", result.Count);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "GetAll: ошибка при получении заказов");
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

    public IList<CounterAgentDto> GetCounterAgents()
    {
        logger.LogInformation("GetCounterAgents: запрос списка контрагентов");
        try
        {
            var result = inner.GetCounterAgents();
            logger.LogInformation("GetCounterAgents: получено {Count} контрагентов", result.Count);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "GetCounterAgents: ошибка при получении контрагентов");
            dialogService.ShowWarning(ex.Message);
            return [];
        }
    }

    public void Create(OrderModel model)
    {
        logger.LogInformation("Create: создание заказа на сумму {Sum}", model.Sum);
        try
        {
            inner.Create(model);
            logger.LogInformation("Create: заказ создан (Id={Id}, Sum={Sum})", model.Id, model.Sum);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Create: ошибка при создании заказа на сумму {Sum}", model.Sum);
            dialogService.ShowWarning(ex.Message);
        }
    }

    public void Update(OrderModel model)
    {
        logger.LogInformation("Update: обновление заказа Id={Id}", model.Id);
        try
        {
            inner.Update(model);
            logger.LogInformation("Update: заказ Id={Id} обновлён", model.Id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Update: ошибка при обновлении заказа Id={Id}", model.Id);
            dialogService.ShowWarning(ex.Message);
        }
    }

    public void Delete(int orderId)
    {
        logger.LogInformation("Delete: удаление заказа Id={OrderId}", orderId);
        try
        {
            inner.Delete(orderId);
            logger.LogInformation("Delete: заказ Id={OrderId} удалён", orderId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Delete: ошибка при удалении заказа Id={OrderId}", orderId);
            dialogService.ShowWarning(ex.Message);
        }
    }
}
