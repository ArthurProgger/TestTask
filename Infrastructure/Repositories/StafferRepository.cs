using Domain;
using NHibernate;

namespace Infrastructure.Repositories;

public class StafferRepository(ISession session)
    : NHibernateRepository<StafferModel>(session), Application.Repositories.IStafferRepository;
