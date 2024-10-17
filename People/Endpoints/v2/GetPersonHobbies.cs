using System.Linq;
using BusinessCard.People.Records;
using Microsoft.EntityFrameworkCore;
using Router;
using Router.Data;
using Router.Data.Configuration.Extensions;
using Router.Data.Extensions;
using Router.Request;
using Router.Response.Extensions;
using Router.Validation.Numbers;

namespace BusinessCard.People.Endpoints.v2
{
    public class GetPersonHobbies : IEndpoint<GetPersonHobbies>
    {
        public void Define(IEndpointBuilder<GetPersonHobbies> configure)
        {
            configure.Get("/api/v2/people/{id}/hobbies")
                .As(data => data.FromUri<int>(a => a.IsAboveZero()))
                .UseData()
                .SetOf<Person>()
                .Select
                (
                    selection: (people, id) => people.Include(a => a.Hobbies).SingleOrDefaultAsync(a => a.Id == id),
                    configure: options => options.ThrowNotFoundIfNothing()
                )
                .MapResult(s => new
                {
                    items = s.Hobbies.Select(hobby => hobby.Title)
                })
                .Respond200Ok();
        }
    }
}