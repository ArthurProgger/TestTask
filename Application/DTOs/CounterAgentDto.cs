namespace Application.DTOs;

public class CounterAgentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Inn { get; set; } = string.Empty;
    public int StafferId { get; set; }
    public string StafferFullName { get; set; } = string.Empty;
}
