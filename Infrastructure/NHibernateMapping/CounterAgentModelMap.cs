using Domain;
using FluentNHibernate.Mapping;

namespace Infrastructure.NHibernateMapping;

public class CounterAgentModelMap : ClassMap<CounterAgentModel>
{
    public CounterAgentModelMap()
    {
        Table("counter_agents");
        Id(x => x.Id).GeneratedBy.Identity();
        Map(x => x.Name).Not.Nullable();
        Map(x => x.Inn).Not.Nullable();
        References(x => x.Staffer).Column("StafferId");
    }
}