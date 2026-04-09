using System.Windows;
using TestTask.ModelView.Editors;

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
