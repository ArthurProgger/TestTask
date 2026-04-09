namespace Application.Services;

public interface IDialogService
{
    bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : class;
    bool Confirm(string message, string title = "Подтверждение");
    void ShowWarning(string message, string title = "Ошибка");
}
