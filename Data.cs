using BusinessCard.People.Records;
using BusinessCard.Technologies.Records;
using Microsoft.EntityFrameworkCore;
using Router.Data.Configuration;

namespace BusinessCard
{
    public class Data : IDataSetup
    {
        public void Configure(IDataScopeBuilder builder)
        {
            builder.With(s => s.Record<Person>()
                .AggregateAs(a => a.Include(i => i.Image)));

            builder.With(s => s.Record<Technology>());
        }
    }
}