using System.Linq;
using BusinessCard.People.Records;
using Microsoft.EntityFrameworkCore;
using Router;
using Router.Cache;
using Router.Data;
using Router.Data.Configuration.Extensions;
using Router.Data.Extensions;
using Router.Helpers;
using Router.Request;
using Router.Response.Extensions;
using Router.Validation.Numbers;

namespace BusinessCard.People.Endpoints.v2
{
    public class GetPersonEducation : IEndpoint<GetPersonEducation>
    {
        public void Define(IEndpointBuilder<GetPersonEducation> configure)
        {
            configure.Get("/api/v2/people/{id}/education")
                .As(s => s.FromUri<int>(a => a.IsAboveZero()))
                .UseData()
                .SetOf<Person>()
                .Select
                (
                    selection: (people, id) =>
                        people.Include(a => a.EducationSteps).SingleOrDefaultAsync(a => a.Id == id),
                    configure: options => options.ThrowNotFoundIfNothing()
                )
                .MapResult(s => new
                {
                    items = s.EducationSteps.Select(e => new
                    {
                        e.Institution,
                        e.Location,
                        e.Name,
                        e.YearStarted,
                        e.YearFinished
                    })
                })
                .Cache(cache => cache.As(id => CacheKey.For("person", "education", ("id", id))).For(2.Hours()))
                .Respond200Ok();
        }
    }
}