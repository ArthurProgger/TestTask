using Domain;
using NHibernate;

namespace Infrastructure.Repositories;

public class CounterAgentRepository(ISession session) : NHibernateRepository<CounterAgentModel>(session),
    Application.Repositories.ICounterAgentRepository;
