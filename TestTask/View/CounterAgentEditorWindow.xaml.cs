using System.Windows;
using TestTask.ModelView.Editors;

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
