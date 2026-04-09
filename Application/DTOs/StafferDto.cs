using Domain;

namespace Application.DTOs;

public class StafferDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public Position Position { get; set; }
    public DateTime Birth { get; set; }
}
