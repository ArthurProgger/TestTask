namespace Domain;

public class CounterAgentModel
{
    public virtual int Id { get; set; }
    public virtual required string Name { get; set; }
    public virtual required string Inn { get; set; }
    public virtual required StafferModel Staffer { get; set; }
}