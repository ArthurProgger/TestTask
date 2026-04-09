using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Application.Services;
using Domain;

namespace TestTask.ModelView.Editors;

public class OrderEditorViewModel : INotifyPropertyChanged, IDataErrorInfo
{
    private int _sum;
    private DateTime _date = DateTime.Today;
    private StafferModel? _selectedStaffer;
    private CounterAgentModel? _selectedCounterAgent;

    public int Sum
    {
        get => _sum;
        set { _sum = value; OnPropertyChanged(nameof(Sum)); }
    }

    public DateTime Date
    {
        get => _date;
        set { _date = value; OnPropertyChanged(nameof(Date)); }
    }

    public StafferModel? SelectedStaffer
    {
        get => _selectedStaffer;
        set { _selectedStaffer = value; OnPropertyChanged(nameof(SelectedStaffer)); }
    }

    public CounterAgentModel? SelectedCounterAgent
    {
        get => _selectedCounterAgent;
        set { _selectedCounterAgent = value; OnPropertyChanged(nameof(SelectedCounterAgent)); }
    }

    public ObservableCollection<StafferModel> Staffers { get; } = new();
    public ObservableCollection<CounterAgentModel> CounterAgents { get; } = new();

    public string Title { get; }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public bool? DialogResult { get; private set; }
    public event Action<bool?>? CloseRequested;

    public OrderEditorViewModel(IOrderService service, OrderModel? existing = null)
    {
        foreach (var s in service.GetStaffers())
            Staffers.Add(s);
        foreach (var c in service.GetCounterAgents())
            CounterAgents.Add(c);

        if (existing != null)
        {
            Title = "Редактирование заказа";
            Sum = existing.Sum;
            Date = existing.Date;
            SelectedStaffer = Staffers.FirstOrDefault(s => s.Id == existing.Staffer?.Id);
            SelectedCounterAgent = CounterAgents.FirstOrDefault(c => c.Id == existing.CounterAgent?.Id);
        }
        else
        {
            Title = "Новый заказ";
        }

        SaveCommand = new RelayCommand(_ => Save(), _ => IsValid);
        CancelCommand = new RelayCommand(_ => Cancel());
    }

    private bool IsValid =>
        string.IsNullOrWhiteSpace(this[nameof(Sum)]) &&
        string.IsNullOrWhiteSpace(this[nameof(SelectedStaffer)]) &&
        string.IsNullOrWhiteSpace(this[nameof(SelectedCounterAgent)]);

    private void Save()
    {
        DialogResult = true;
        CloseRequested?.Invoke(true);
    }

    private void Cancel()
    {
        DialogResult = false;
        CloseRequested?.Invoke(false);
    }

    public string Error => string.Empty;

    public string this[string columnName] => columnName switch
    {
        nameof(Sum) => Sum <= 0 ? "Сумма должна быть больше 0" : string.Empty,
        nameof(SelectedStaffer) => SelectedStaffer == null ? "Выберите сотрудника" : string.Empty,
        nameof(SelectedCounterAgent) => SelectedCounterAgent == null ? "Выберите контрагента" : string.Empty,
        _ => string.Empty
    };

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
