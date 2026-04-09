namespace Application.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public int Sum { get; set; }
    public DateTime Date { get; set; }
    public int StafferId { get; set; }
    public string StafferFullName { get; set; } = string.Empty;
    public int CounterAgentId { get; set; }
    public string CounterAgentName { get; set; } = string.Empty;
}
