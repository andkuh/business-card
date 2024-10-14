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
    public class GetPersonEducation : IEndpoint<GetPersonEducation>
    {
        public void Define(IEndpointBuilder<GetPersonEducation> configure)
        {
            configure.Get("/api/v2/people/{id}/education")
                .As(s => s.FromUri<int>())
                .UseData()
                .SetOf<Person>()
                .Select((s, req) => s.Include(a => a.EducationSteps).SingleOrDefaultAsync(a => a.Id == req))
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
                .Respond200Ok();
        }
    }
}