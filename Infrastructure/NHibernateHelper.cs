using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infrastructure.NHibernateMapping;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Infrastructure;

public class NHibernateHelper
{
    private static ISessionFactory? _sessionFactory;

    public static ISessionFactory SessionFactory =>
        _sessionFactory ??= CreateSessionFactory();

    private static ISessionFactory CreateSessionFactory()
    {
        return Fluently.Configure()
            .Database(
                MySQLConfiguration.Standard
                    .ConnectionString(c => c
                        .Server("localhost")
                        .Port(3306)
                        .Database("test_db")
                        .Username("root")
                        .Password("test_pass"))
                    .ShowSql()
            )
            .Mappings(m => m.FluentMappings
                .AddFromAssemblyOf<StafferModelMap>()
                .AddFromAssemblyOf<CounterAgentModelMap>()
                .AddFromAssemblyOf<OrderModelMap>())
            .ExposeConfiguration(cfg =>
            {
                var schemaUpdate = new SchemaUpdate(cfg);
                schemaUpdate.Execute(useStdOut: true, doUpdate: true);
            })
            .BuildSessionFactory();
    }

    public static ISession OpenSession() => SessionFactory.OpenSession();
}
