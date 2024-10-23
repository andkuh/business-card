using System.Linq;
using BusinessCard.People.Records;
using Microsoft.EntityFrameworkCore;
using Router;
using Router.Cache;
using Router.Data;
using Router.Data.Configuration.Extensions;
using Router.Helpers;
using Router.Request;
using Router.Response.Extensions;
using Router.Validation.Numbers;

namespace BusinessCard.People.Endpoints.v2
{
    public class GetPersonLinks : IEndpoint<GetPersonLinks>
    {
        public void Define(IEndpointBuilder<GetPersonLinks> configure)
        {
            configure.Get("/api/v2/people/{id}/links")
                .As(data => new
                {
                    id = data.FromUri<int>(id => id.IsAboveZero())
                })
                .UseData()
                .SetOf<Person>()
                .Select
                (
                    selection: (queryable, request) =>
                    {
                        return queryable
                            .Include(s => s.Links)
                            .FirstOrDefaultAsync(s => s.Id == request.id);
                    },
                    configure: options => options.ThrowNotFoundIfNothing()
                )
                .MapResult(person => new
                {
                    items = person!.Links.OrderBy(i => i.Ordinal).Select(l => new
                    {
                        l.Type, l.Value
                    })
                })
                .Cache(cache => cache.As(id => CacheKey.For("person", "links", ("id", id))).For(15.Minutes()))
                .Respond200Ok();
        }
    }
}