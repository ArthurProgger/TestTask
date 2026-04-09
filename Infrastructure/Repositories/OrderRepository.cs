using Domain;
using NHibernate;

namespace Infrastructure.Repositories;

public class OrderRepository : NHibernateRepository<OrderModel>, Application.Repositories.IOrderRepository
{
    public OrderRepository(ISession session) : base(session)
    {
    }
}
