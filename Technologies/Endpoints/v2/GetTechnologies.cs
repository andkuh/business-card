using System.Linq;
using BusinessCard.Technologies.Records;
using Microsoft.EntityFrameworkCore;
using Router;
using Router.Data;
using Router.Response.Extensions;

namespace BusinessCard.Technologies.Endpoints.v2
{
    public class GetTechnologies : IEndpoint<GetTechnologies>
    {
        public void Define(IEndpointBuilder<GetTechnologies> configure)
        {
            configure.Get("/api/v2/technologies")
                .AsNoParams()
                .UseData()
                .SetOf<Technology>()
                .Select(a => a.OrderBy(s => s.Title).Select(t => new {t.Title}).ToListAsync())
                .Map(items => new {items})
                .Respond200Ok();
        }
    }
}