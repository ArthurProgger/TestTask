using System.ComponentModel;
using System.Windows.Input;
using Application.DTOs;
using Domain;

namespace TestTask.ViewModel.Editors;

public class StafferEditorViewModel : INotifyPropertyChanged, IDataErrorInfo
{
    private string _fullName = string.Empty;
    private Position _position;
    private DateTime _birth = DateTime.Today.AddYears(-25);

    public string FullName
    {
        get => _fullName;
        set
        {
            _fullName = value;
            OnPropertyChanged(nameof(FullName));
        }
    }

    public Position Position
    {
        get => _position;
        set
        {
            _position = value;
            OnPropertyChanged(nameof(Position));
        }
    }

    public DateTime Birth
    {
        get => _birth;
        set
        {
            _birth = value;
            OnPropertyChanged(nameof(Birth));
        }
    }

    public IEnumerable<Position> Positions => Enum.GetValues<Position>();

    public string Title { get; }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public bool? DialogResult { get; private set; }
    public event Action<bool?>? CloseRequested;

    public StafferEditorViewModel(StafferDto? existing = null)
    {
        if (existing != null)
        {
            Title = "Редактирование сотрудника";
            FullName = existing.FullName;
            Position = existing.Position;
            Birth = existing.Birth;
        }
        else
        {
            Title = "Новый сотрудник";
        }

        SaveCommand = new RelayCommand(_ => Save(), _ => IsValid);
        CancelCommand = new RelayCommand(_ => Cancel());
    }

    private bool IsValid =>
        string.IsNullOrWhiteSpace(this[nameof(FullName)]) && string.IsNullOrWhiteSpace(this[nameof(Birth)]);

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
            nameof(FullName) => string.IsNullOrWhiteSpace(FullName) ? "ФИО обязательно" : string.Empty,
            nameof(Birth) => Birth >= DateTime.Today ? "Дата рождения должна быть в прошлом" : string.Empty,
            _ => string.Empty
        };

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}