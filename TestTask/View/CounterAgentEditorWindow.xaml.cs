using System.Windows;
using TestTask.ViewModel.Editors;

namespace TestTask.View;

public partial class CounterAgentEditorWindow : Window
{
    public CounterAgentEditorWindow(CounterAgentEditorViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        viewModel.CloseRequested += result => DialogResult = result;
    }
}
