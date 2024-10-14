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
                .MapResult(s => new PersonDto(s.Id, s.Location, s.FirstName, s.LastName, s.YearsOld)
                {
                    Summary = s.Summary,
                    Specialization = s.Specialization,
                    Image = new PersonDto.ImageDto()
                    {
                        Bytes = s.Image.Bytes, ContentType = s.Image.ContentType
                    }
                })
                .Respond200Ok();
        }
    }
}