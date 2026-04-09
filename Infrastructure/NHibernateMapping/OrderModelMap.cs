using Domain;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;

namespace Infrastructure.NHibernateMapping;

public class OrderModelMap : ClassMap<OrderModel>
{
    public OrderModelMap()
    {
        Table("orders");
        Id(x => x.Id).GeneratedBy.Identity();
        Map(x => x.Sum);
        Map(x => x.Date);
        References(x => x.Staffer).Column("StafferId");
        References(x => x.CounterAgent).Column("CounterAgentId");
    }
}