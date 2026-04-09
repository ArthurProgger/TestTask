using System.Windows;
using Application.Repositories;
using Application.Services;
using Infrastructure.Decorators;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TestTask.ModelView;

namespace TestTask;

public partial class App : System.Windows.Application
{
    public static IServiceProvider Services { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();

        services.AddLogging(builder => builder.AddConsole());

        services.AddTransient<IUnitOfWork, NHibernateUnitOfWork>();
        services.AddSingleton<Func<IUnitOfWork>>(sp => () => sp.GetRequiredService<IUnitOfWork>());

        services.AddTransient<StafferService>();
        services.AddTransient<IStafferService>(sp => new LoggingStafferService(
            sp.GetRequiredService<StafferService>(),
            sp.GetRequiredService<ILogger<LoggingStafferService>>(),
            sp.GetRequiredService<IDialogService>()));

        services.AddTransient<CounterAgentService>();
        services.AddTransient<ICounterAgentService>(sp => new LoggingCounterAgentService(
            sp.GetRequiredService<CounterAgentService>(),
            sp.GetRequiredService<ILogger<LoggingCounterAgentService>>(),
            sp.GetRequiredService<IDialogService>()));

        services.AddTransient<OrderService>();
        services.AddTransient<IOrderService>(sp => new LoggingOrderService(
            sp.GetRequiredService<OrderService>(),
            sp.GetRequiredService<ILogger<LoggingOrderService>>(),
            sp.GetRequiredService<IDialogService>()));

        services.AddSingleton<IDialogService, TestTask.Services.DialogService>();

        services.AddTransient<MainWindowViewModel>();

        Services = services.BuildServiceProvider();

        var mainWindow = new MainWindow(Services.GetRequiredService<MainWindowViewModel>());
        mainWindow.Show();
    }
}
