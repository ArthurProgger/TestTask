using Domain;

namespace Application.DTOs;

public static class DtoMappingExtensions
{
    public static StafferDto ToDto(this StafferModel model) => new()
    {
        Id = model.Id,
        FullName = model.FullName,
        Position = model.Position,
        Birth = model.Birth
    };

    public static CounterAgentDto ToDto(this CounterAgentModel model) => new()
    {
        Id = model.Id,
        Name = model.Name,
        Inn = model.Inn,
        StafferId = model.Staffer?.Id ?? 0,
        StafferFullName = model.Staffer?.FullName ?? string.Empty
    };

    public static OrderDto ToDto(this OrderModel model) => new()
    {
        Id = model.Id,
        Sum = model.Sum,
        Date = model.Date,
        StafferId = model.Staffer?.Id ?? 0,
        StafferFullName = model.Staffer?.FullName ?? string.Empty,
        CounterAgentId = model.CounterAgent?.Id ?? 0,
        CounterAgentName = model.CounterAgent?.Name ?? string.Empty
    };
}
