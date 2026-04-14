using System.Windows;
using TestTask.ViewModel.Editors;

namespace TestTask.View;

public partial class StafferEditorWindow : Window
{
    public StafferEditorWindow(StafferEditorViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        viewModel.CloseRequested += result => DialogResult = result;
    }
}
