using BusinessCard.People.Records;
using Router;
using Router.Data;
using Router.Data.Configuration.Single.Extensions;
using Router.Request;
using Router.Response.Extensions;

namespace BusinessCard.People.Endpoints.v2
{
    public class GetPerson : IEndpoint<GetPerson>
    {
        public void Define(IEndpointBuilder<GetPerson> configure)
        {
            configure.Get("/api/v2/people/{id}")
                .As(s => s.FromUri<int>())
                .UseData()
                .SetOf<Person>()
                .Single(item => item.Having(id => person => person.Id == id))
                .MapResult(person => new
                {
                    person.Id, 
                    person.Location, 
                    person.FirstName, 
                    person.LastName, 
                    person.YearsOld,
                    person.Summary,
                    person.Specialization,
                    image = new
                    {
                        person.Image.Bytes, 
                        person.Image.ContentType
                    }
                })
                .Respond200Ok();
        }
    }
}