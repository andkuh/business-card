using System.Linq;
using System.Threading.Tasks;
using BusinessCard.Employers.UseCases.GetEmployments;
using Microsoft.AspNetCore.Mvc;

namespace BusinessCard.Employers.Endpoints
{
    [ApiController]
    [Route("/api/people")]
    public class GetEmploymentsController : Controller
    {
        private readonly IGetEmployments _getEmployments;
        private readonly IModelToDtoMapper _mapper;

        public GetEmploymentsController(IGetEmployments getEmployments, IModelToDtoMapper mapper)
        {
            _getEmployments = getEmployments;
            _mapper = mapper;
        }

        [HttpGet("{id:int}/employments")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var models = await _getEmployments.Get(new Query(id)).ConfigureAwait(false);

            var items = models.Select(s => _mapper.Map(s));
            
            return Ok(new {items});
        }
    }
}