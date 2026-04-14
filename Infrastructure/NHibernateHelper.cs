using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infrastructure.NHibernateMapping;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Infrastructure;

public class NHibernateHelper
{
    private readonly ISessionFactory _sessionFactory;

    public NHibernateHelper(string connectionString)
    {
        _sessionFactory = Fluently.Configure()
            .Database(
                MySQLConfiguration.Standard
                    .ConnectionString(connectionString)
            )
            .Mappings(m => m.FluentMappings
                .AddFromAssemblyOf<StafferModelMap>())
            .ExposeConfiguration(cfg =>
            {
                var schemaUpdate = new SchemaUpdate(cfg);
                schemaUpdate.Execute(useStdOut: false, doUpdate: true);
            })
            .BuildSessionFactory();
    }

    public ISession OpenSession() => _sessionFactory.OpenSession();
}
