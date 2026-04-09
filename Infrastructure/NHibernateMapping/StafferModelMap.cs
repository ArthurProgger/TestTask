using Domain;
using FluentNHibernate.Mapping;

namespace Infrastructure.NHibernateMapping;

public class StafferModelMap : ClassMap<StafferModel>
{
    public StafferModelMap()
    {
        Table("staffers");
        Id(x => x.Id).GeneratedBy.Identity();
        Map(x => x.FullName).Not.Nullable();
        Map(x => x.Position).CustomType<int>();
        Map(x => x.Birth);
        HasMany(x => x.CounterAgents)
            .KeyColumn("StafferId")
            .Cascade.AllDeleteOrphan();
    }
}