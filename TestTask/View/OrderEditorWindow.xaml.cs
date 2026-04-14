using System.Windows;
using TestTask.ViewModel.Editors;

namespace TestTask.View;

public partial class OrderEditorWindow : Window
{
    public OrderEditorWindow(OrderEditorViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        viewModel.CloseRequested += result => DialogResult = result;
    }
}
