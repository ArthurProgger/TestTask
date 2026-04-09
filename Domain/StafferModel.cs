namespace Domain;

public class StafferModel
{
    public virtual int Id { get; set; }
    public virtual required string FullName { get; set; }
    public virtual Position Position { get; set; }
    public virtual DateTime Birth { get; set; }
    public virtual IList<CounterAgentModel> CounterAgents { get; set; } = new List<CounterAgentModel>();
}

public enum Position
{
    Director,
    Employer
}