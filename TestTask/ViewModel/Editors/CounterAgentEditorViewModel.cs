using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Application.DTOs;
using Application.Services;

namespace TestTask.ViewModel.Editors;

public partial class CounterAgentEditorViewModel : INotifyPropertyChanged, IDataErrorInfo
{
    private string _name = string.Empty;
    private string _inn = string.Empty;
    private StafferDto? _selectedStaffer;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public string Inn
    {
        get => _inn;
        set
        {
            _inn = value;
            OnPropertyChanged(nameof(Inn));
        }
    }

    public StafferDto? SelectedStaffer
    {
        get => _selectedStaffer;
        set
        {
            _selectedStaffer = value;
            OnPropertyChanged(nameof(SelectedStaffer));
        }
    }

    public ObservableCollection<StafferDto> Staffers { get; } = new();

    public string Title { get; }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public bool? DialogResult { get; private set; }
    public event Action<bool?>? CloseRequested;

    public CounterAgentEditorViewModel(ICounterAgentService service, CounterAgentDto? existing = null)
    {
        foreach (var s in service.GetStaffers()) Staffers.Add(s);

        if (existing != null)
        {
            Title = "Редактирование контрагента";
            Name = existing.Name;
            Inn = existing.Inn;
            SelectedStaffer = Staffers.FirstOrDefault(s => s.Id == existing.StafferId);
        }
        else
        {
            Title = "Новый контрагент";
        }

        SaveCommand = new RelayCommand(_ => Save(), _ => IsValid);
        CancelCommand = new RelayCommand(_ => Cancel());
    }

    private bool IsValid =>
        string.IsNullOrWhiteSpace(this[nameof(Name)]) && string.IsNullOrWhiteSpace(this[nameof(Inn)]) &&
        string.IsNullOrWhiteSpace(this[nameof(SelectedStaffer)]);

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

    public string this[string columnName] =>
        columnName switch
        {
            nameof(Name) => string.IsNullOrWhiteSpace(Name) ? "Название обязательно" : string.Empty,
            nameof(Inn) => !InnRegex().IsMatch(Inn ?? "") ? "ИНН должен содержать 10 или 12 цифр" : string.Empty,
            nameof(SelectedStaffer) => SelectedStaffer == null ? "Выберите сотрудника" : string.Empty,
            _ => string.Empty
        };

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    [GeneratedRegex(@"^\d{10}(\d{2})?$")]
    private static partial Regex InnRegex();
}