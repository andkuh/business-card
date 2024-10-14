using System.Linq;
using System.Threading.Tasks;
using BusinessCard.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusinessCard.Technologies.Endpoints
{
    [ApiController]
    [Route("api/technologies")]
    public class GetTechnologies : Controller
    {
        private readonly Ctx _context;

        public GetTechnologies(Ctx context)
        {
            _context = context;
        }


        public async Task<IActionResult> Get()
        {
            var technologies =
                await _context.Technologies.OrderBy(s => s.Title).Select(s => new {s.Title}).ToListAsync();

            return Ok(new {items = technologies});
        }
    }
}