using System.Windows;
using TestTask.ModelView.Editors;

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
