namespace Domain;

public class OrderModel
{
    public virtual int Id { get; set; }
    public virtual int Sum { get; set; }
    public virtual DateTime Date { get; set; }
    public virtual StafferModel Staffer { get; set; }
    public virtual CounterAgentModel CounterAgent { get; set; }
}