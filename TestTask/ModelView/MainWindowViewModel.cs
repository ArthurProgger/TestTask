using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Application.DTOs;
using Application.Services;
using Domain;
using TestTask.ModelView.Editors;

namespace TestTask.ModelView;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly IStafferService _stafferService;
    private readonly ICounterAgentService _counterAgentService;
    private readonly IOrderService _orderService;
    private readonly IDialogService _dialogService;

    public ObservableCollection<StafferDto> Staffers { get; } = new();
    public ObservableCollection<CounterAgentDto> CounterAgents { get; } = new();
    public ObservableCollection<OrderDto> Orders { get; } = new();

    private StafferDto? _selectedStaffer;
    public StafferDto? SelectedStaffer
    {
        get => _selectedStaffer;
        set => SetField(ref _selectedStaffer, value);
    }

    private CounterAgentDto? _selectedCounterAgent;
    public CounterAgentDto? SelectedCounterAgent
    {
        get => _selectedCounterAgent;
        set => SetField(ref _selectedCounterAgent, value);
    }

    private OrderDto? _selectedOrder;
    public OrderDto? SelectedOrder
    {
        get => _selectedOrder;
        set => SetField(ref _selectedOrder, value);
    }

    public ICommand AddStafferCommand { get; }
    public ICommand EditStafferCommand { get; }
    public ICommand DeleteStafferCommand { get; }

    public ICommand AddCounterAgentCommand { get; }
    public ICommand EditCounterAgentCommand { get; }
    public ICommand DeleteCounterAgentCommand { get; }

    public ICommand AddOrderCommand { get; }
    public ICommand EditOrderCommand { get; }
    public ICommand DeleteOrderCommand { get; }

    public MainWindowViewModel(
        IStafferService stafferService,
        ICounterAgentService counterAgentService,
        IOrderService orderService,
        IDialogService dialogService)
    {
        _stafferService = stafferService;
        _counterAgentService = counterAgentService;
        _orderService = orderService;
        _dialogService = dialogService;

        AddStafferCommand = new RelayCommand(_ => AddStaffer());
        EditStafferCommand = new RelayCommand(_ => EditStaffer(), _ => SelectedStaffer != null);
        DeleteStafferCommand = new RelayCommand(_ => DeleteStaffer(), _ => SelectedStaffer != null);

        AddCounterAgentCommand = new RelayCommand(_ => AddCounterAgent());
        EditCounterAgentCommand = new RelayCommand(_ => EditCounterAgent(), _ => SelectedCounterAgent != null);
        DeleteCounterAgentCommand = new RelayCommand(_ => DeleteCounterAgent(), _ => SelectedCounterAgent != null);

        AddOrderCommand = new RelayCommand(_ => AddOrder());
        EditOrderCommand = new RelayCommand(_ => EditOrder(), _ => SelectedOrder != null);
        DeleteOrderCommand = new RelayCommand(_ => DeleteOrder(), _ => SelectedOrder != null);

        LoadData();
    }

    private void LoadData()
    {
        foreach (var item in _stafferService.GetAll())
            Staffers.Add(item);

        foreach (var item in _counterAgentService.GetAll())
            CounterAgents.Add(item);

        foreach (var item in _orderService.GetAll())
            Orders.Add(item);
    }

    private void AddStaffer()
    {
        var vm = new StafferEditorViewModel();
        if (_dialogService.ShowDialog(vm) != true) return;

        var model = new StafferModel
        {
            FullName = vm.FullName,
            Position = vm.Position,
            Birth = vm.Birth
        };
        _stafferService.Create(model);

        var dto = model.ToDto();
        Staffers.Add(dto);
    }

    private void EditStaffer()
    {
        if (SelectedStaffer == null) return;
        var vm = new StafferEditorViewModel(SelectedStaffer);
        if (_dialogService.ShowDialog(vm) != true) return;

        var model = new StafferModel
        {
            Id = SelectedStaffer.Id,
            FullName = vm.FullName,
            Position = vm.Position,
            Birth = vm.Birth
        };
        _stafferService.Update(model);

        SelectedStaffer.FullName = vm.FullName;
        SelectedStaffer.Position = vm.Position;
        SelectedStaffer.Birth = vm.Birth;
        RefreshCollection(Staffers);
    }

    private void DeleteStaffer()
    {
        if (SelectedStaffer == null) return;

        if (!_stafferService.CanDelete(SelectedStaffer.Id))
        {
            _dialogService.ShowWarning(
                "Невозможно удалить сотрудника — есть связанные заказы или контрагенты. Сначала удалите их.");
            return;
        }

        if (!_dialogService.Confirm($"Удалить сотрудника \"{SelectedStaffer.FullName}\"?"))
            return;

        _stafferService.Delete(SelectedStaffer.Id);
        Staffers.Remove(SelectedStaffer);
    }

    private void AddCounterAgent()
    {
        var vm = new CounterAgentEditorViewModel(_counterAgentService);
        if (_dialogService.ShowDialog(vm) != true) return;

        var model = new CounterAgentModel
        {
            Name = vm.Name,
            Inn = vm.Inn,
            Staffer = new StafferModel
            {
                Id = vm.SelectedStaffer!.Id,
                FullName = vm.SelectedStaffer.FullName,
                Position = vm.SelectedStaffer.Position,
                Birth = vm.SelectedStaffer.Birth
            }
        };
        _counterAgentService.Create(model);

        var dto = model.ToDto();
        CounterAgents.Add(dto);
    }

    private void EditCounterAgent()
    {
        if (SelectedCounterAgent == null) return;
        var vm = new CounterAgentEditorViewModel(_counterAgentService, SelectedCounterAgent);
        if (_dialogService.ShowDialog(vm) != true) return;

        var model = new CounterAgentModel
        {
            Id = SelectedCounterAgent.Id,
            Name = vm.Name,
            Inn = vm.Inn,
            Staffer = new StafferModel
            {
                Id = vm.SelectedStaffer!.Id,
                FullName = vm.SelectedStaffer.FullName,
                Position = vm.SelectedStaffer.Position,
                Birth = vm.SelectedStaffer.Birth
            }
        };
        _counterAgentService.Update(model);

        SelectedCounterAgent.Name = vm.Name;
        SelectedCounterAgent.Inn = vm.Inn;
        SelectedCounterAgent.StafferId = vm.SelectedStaffer!.Id;
        SelectedCounterAgent.StafferFullName = vm.SelectedStaffer.FullName;
        RefreshCollection(CounterAgents);
    }

    private void DeleteCounterAgent()
    {
        if (SelectedCounterAgent == null) return;

        if (!_counterAgentService.CanDelete(SelectedCounterAgent.Id))
        {
            _dialogService.ShowWarning(
                "Невозможно удалить контрагента — есть связанные заказы. Сначала удалите их.");
            return;
        }

        if (!_dialogService.Confirm($"Удалить контрагента \"{SelectedCounterAgent.Name}\"?"))
            return;

        _counterAgentService.Delete(SelectedCounterAgent.Id);
        CounterAgents.Remove(SelectedCounterAgent);
    }

    private void AddOrder()
    {
        var vm = new OrderEditorViewModel(_orderService);
        if (_dialogService.ShowDialog(vm) != true) return;

        var staffer = new StafferModel
        {
            Id = vm.SelectedStaffer!.Id,
            FullName = vm.SelectedStaffer.FullName,
            Position = vm.SelectedStaffer.Position,
            Birth = vm.SelectedStaffer.Birth
        };
        var model = new OrderModel
        {
            Sum = vm.Sum,
            Date = vm.Date,
            Staffer = staffer,
            CounterAgent = new CounterAgentModel
            {
                Id = vm.SelectedCounterAgent!.Id,
                Name = vm.SelectedCounterAgent.Name,
                Inn = vm.SelectedCounterAgent.Inn,
                Staffer = staffer
            }
        };
        _orderService.Create(model);

        var dto = model.ToDto();
        Orders.Add(dto);
    }

    private void EditOrder()
    {
        if (SelectedOrder == null) return;
        var vm = new OrderEditorViewModel(_orderService, SelectedOrder);
        if (_dialogService.ShowDialog(vm) != true) return;

        var staffer = new StafferModel
        {
            Id = vm.SelectedStaffer!.Id,
            FullName = vm.SelectedStaffer.FullName,
            Position = vm.SelectedStaffer.Position,
            Birth = vm.SelectedStaffer.Birth
        };
        
        var model = new OrderModel
        {
            Id = SelectedOrder.Id,
            Sum = vm.Sum,
            Date = vm.Date,
            Staffer = staffer,
            CounterAgent = new CounterAgentModel
            {
                Id = vm.SelectedCounterAgent!.Id,
                Name = vm.SelectedCounterAgent.Name,
                Inn = vm.SelectedCounterAgent.Inn,
                Staffer = staffer
            }
        };
        
        _orderService.Update(model);

        SelectedOrder.Sum = vm.Sum;
        SelectedOrder.Date = vm.Date;
        SelectedOrder.StafferId = vm.SelectedStaffer!.Id;
        SelectedOrder.StafferFullName = vm.SelectedStaffer.FullName;
        SelectedOrder.CounterAgentId = vm.SelectedCounterAgent!.Id;
        SelectedOrder.CounterAgentName = vm.SelectedCounterAgent.Name;
        RefreshCollection(Orders);
    }

    private void DeleteOrder()
    {
        if (SelectedOrder == null) return;
        if (!_dialogService.Confirm($"Удалить заказ #{SelectedOrder.Id} на сумму {SelectedOrder.Sum}?"))
            return;

        _orderService.Delete(SelectedOrder.Id);
        Orders.Remove(SelectedOrder);
    }

    private static void RefreshCollection<T>(ObservableCollection<T> collection)
    {
        var items = collection.ToList();
        collection.Clear();
        foreach (var item in items)
            collection.Add(item);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
