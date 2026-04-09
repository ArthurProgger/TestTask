using Domain;
using NHibernate;

namespace Infrastructure.Repositories;

public class StafferRepository : NHibernateRepository<StafferModel>, Application.Repositories.IStafferRepository
{
    public StafferRepository(ISession session) : base(session)
    {
    }
}
