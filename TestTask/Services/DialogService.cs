using System.Windows;
using Application.Services;
using TestTask.ViewModel.Editors;
using TestTask.View;

namespace TestTask.Services;

public class DialogService : IDialogService
{
    private static readonly Dictionary<Type, Func<object, Window>> WindowFactories = new()
    {
        [typeof(StafferEditorViewModel)] = vm => new StafferEditorWindow((StafferEditorViewModel)vm),
        [typeof(CounterAgentEditorViewModel)] = vm => new CounterAgentEditorWindow((CounterAgentEditorViewModel)vm),
        [typeof(OrderEditorViewModel)] = vm => new OrderEditorWindow((OrderEditorViewModel)vm),
    };

    public bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : class
    {
        if (!WindowFactories.TryGetValue(typeof(TViewModel), out var factory))
            throw new InvalidOperationException($"No window registered for {typeof(TViewModel).Name}");

        var window = factory(viewModel);
        window.Owner = System.Windows.Application.Current.MainWindow;
        return window.ShowDialog();
    }

    public bool Confirm(string message, string title = "Подтверждение")
    {
        return MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question) ==
               MessageBoxResult.Yes;
    }

    public void ShowWarning(string message, string title = "Ошибка")
    {
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
    }
}