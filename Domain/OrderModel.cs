namespace Domain;

public class OrderModel
{
    public virtual int Id { get; set; }
    public virtual decimal Sum { get; set; }
    public virtual DateTime Date { get; set; }
    public virtual required StafferModel Staffer { get; set; }
    public virtual required CounterAgentModel CounterAgent { get; set; }
}