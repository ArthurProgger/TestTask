using Domain;
using NHibernate;

namespace Infrastructure.Repositories;

public class CounterAgentRepository : NHibernateRepository<CounterAgentModel>, Application.Repositories.ICounterAgentRepository
{
    public CounterAgentRepository(ISession session) : base(session)
    {
    }
}
