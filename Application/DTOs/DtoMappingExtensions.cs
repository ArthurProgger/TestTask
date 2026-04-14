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

    public static StafferModel ToModel(this StafferDto dto) => new()
    {
        Id = dto.Id,
        FullName = dto.FullName,
        Position = dto.Position,
        Birth = dto.Birth
    };

    public static CounterAgentModel ToModel(this CounterAgentDto dto, StafferModel staffer) => new()
    {
        Id = dto.Id,
        Name = dto.Name,
        Inn = dto.Inn,
        Staffer = staffer
    };

    public static OrderModel ToModel(this OrderDto dto, StafferModel staffer, CounterAgentModel counterAgent) => new()
    {
        Id = dto.Id,
        Sum = dto.Sum,
        Date = dto.Date,
        Staffer = staffer,
        CounterAgent = counterAgent
    };
}
