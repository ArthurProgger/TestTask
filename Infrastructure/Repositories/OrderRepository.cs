using Domain;
using NHibernate;

namespace Infrastructure.Repositories;

public class OrderRepository(ISession session)
    : NHibernateRepository<OrderModel>(session), Application.Repositories.IOrderRepository;
