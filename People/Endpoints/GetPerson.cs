using System.Threading.Tasks;
using BusinessCard.Infrastructure;
using BusinessCard.People.Records;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusinessCard.People.Endpoints
{
    [ApiController]
    [Route("api/people")]
    public class GetPerson : Controller
    {
        private readonly Ctx _context;

        public GetPerson(Ctx context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute(Name = "id")] int id)
        {
            var person = await _context.Set<Person>().Include(s => s.Image).SingleOrDefaultAsync(s => s.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            var result = new PersonDto(person.Id, person.Location, person.FirstName, person.LastName, person.YearsOld)
            {
                Specialization = person.Specialization,
                Summary = person.Summary,
                Image = new PersonDto.ImageDto()
                {
                    Bytes = person.Image.Bytes,
                    ContentType = person.Image.ContentType
                }
            };

            return Ok(result);
        }
    }

    public class PersonDto
    {
        public int Id { get; }
        public string Location { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public int YearsOld { get; }
        public string Specialization { get; set; }
        public string Summary { get; set; }

        public ImageDto Image { get; set; }

        public PersonDto(int id, string location, string firstName, string lastName, int yearsOld)
        {
            Id = id;
            Location = location;
            FirstName = firstName;
            LastName = lastName;
            YearsOld = yearsOld;
        }

        public class ImageDto
        {
            public byte[] Bytes { get; set; }
            public string ContentType { get; set; }
        }
    }
}