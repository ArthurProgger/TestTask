using System.Windows;
using Application.Repositories;
using Application.Services;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using TestTask.ModelView;

namespace TestTask;

public partial class App : System.Windows.Application
{
    public static IServiceProvider Services { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();

        services.AddTransient<IUnitOfWork, NHibernateUnitOfWork>();
        services.AddSingleton<Func<IUnitOfWork>>(sp => () => sp.GetRequiredService<IUnitOfWork>());

        services.AddTransient<IStafferService, StafferService>();
        services.AddTransient<ICounterAgentService, CounterAgentService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddSingleton<IDialogService, TestTask.Services.DialogService>();

        services.AddTransient<MainWindowViewModel>();

        Services = services.BuildServiceProvider();

        var mainWindow = new MainWindow(Services.GetRequiredService<MainWindowViewModel>());
        mainWindow.Show();
    }
}
