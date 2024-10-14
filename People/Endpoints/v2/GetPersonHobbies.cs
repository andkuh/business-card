using System.Linq;
using BusinessCard.People.Records;
using Microsoft.EntityFrameworkCore;
using Router;
using Router.Data;
using Router.Data.Extensions;
using Router.Request;
using Router.Response.Extensions;

namespace BusinessCard.People.Endpoints.v2
{
    public class GetPersonHobbies : IEndpoint<GetPersonHobbies>
    {
        public void Define(IEndpointBuilder<GetPersonHobbies> configure)
        {
            configure.Get("/api/v2/people/{id}/hobbies")
                .As(s => s.FromUri<int>())
                .UseData()
                .SetOf<Person>()
                .Select((s, req) => s.Include(a => a.Hobbies).SingleOrDefaultAsync(a => a.Id == req))
                .MapResult(s => new
                {
                    items = Enumerable.Select<Hobby, string>(s.Hobbies, e => e.Title)
                })
                .Respond200Ok();
        }
    }
}