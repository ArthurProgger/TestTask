using Application.Services;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Decorators;

public abstract class LoggingServiceBase(ILogger logger, IDialogService dialogService)
{
    protected T Execute<T>(Func<T> action, T fallback, string enterMsg, string successMsg, string errorMsg)
    {
        logger.LogInformation(enterMsg);
        try
        {
            var result = action();
            logger.LogInformation(successMsg);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, errorMsg);
            dialogService.ShowWarning(ex.Message);
            throw;
        }
    }

    protected void Execute(Action action, string enterMsg, string successMsg, string errorMsg)
    {
        logger.LogInformation(enterMsg);
        try
        {
            action();
            logger.LogInformation(successMsg);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, errorMsg);
            dialogService.ShowWarning(ex.Message);
            throw;
        }
    }
}
