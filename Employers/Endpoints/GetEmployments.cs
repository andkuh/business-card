using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusinessCard.Employments.Records;
using BusinessCard.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusinessCard.Employers.Endpoints
{
    [ApiController]
    [Route("/api/people")]
    public class GetEmployments : Controller
    {
        private readonly Ctx _context;

        public GetEmployments(Ctx context)
        {
            _context = context;
        }

        [HttpGet("{id:int}/employments")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var set = _context.Set<Employment>();

            var queryable = set
                .Include(s => s.Employer)
                .Include(s => s.Person)
                .Include(s => s.JobTitles)
                .Include(s => s.Assignments)
                .ThenInclude(s => s.Technologies)
                .Include(s => s.Assignments)
                .ThenInclude(s => s.Duties)
                .OrderByDescending(s => s.EndDate)
                .Where(s => s.PersonId == id)
                .AsNoTracking();

            var employments = await queryable.OrderByDescending(s => s.StartDate)
                .Select(s => new EmploymentDto
                {
                    Id = s.Id,
                    Employer = new EmploymentDto.EmployerDto
                    {
                        Id = s.Employer.Id,
                        Name = s.Employer.Name
                    },
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    CareerSteps = s.JobTitles.OrderByDescending(j => j.StartDate).Select(jobTitle =>
                        new EmploymentDto.CareerStep
                        {
                            Title = jobTitle.Name,
                            StartDate = jobTitle.StartDate,
                            EndDate = jobTitle.EndDate,
                            Assignments = s.Assignments.OrderByDescending(a => a.StartDate)
                                .Where
                                (
                                    assignment =>
                                        assignment.StartDate >= jobTitle.StartDate &&
                                        assignment.EndDate <= jobTitle.EndDate ||
                                        assignment.StartDate <= jobTitle.StartDate &&
                                        assignment.EndDate <= jobTitle.EndDate &&
                                        assignment.EndDate >= jobTitle.StartDate ||
                                        assignment.StartDate <= jobTitle.StartDate &&
                                        assignment.EndDate >= jobTitle.EndDate ||
                                        assignment.StartDate >= jobTitle.StartDate
                                        && assignment.EndDate >= jobTitle.EndDate
                                )
                                .Select(a => new EmploymentDto.AssignmentDto()
                                {
                                    Description = a.Description,
                                    Id = a.Id,
                                    Name = a.Name,
                                    StartDate = a.StartDate < jobTitle.StartDate ? jobTitle.StartDate : a.StartDate,
                                    EndDate = a.EndDate > jobTitle.EndDate ? jobTitle.EndDate : a.EndDate,
                                    Summary = a.Summary,
                                    Technologies = a.Technologies.OrderBy(o => o.Title).Select(t => t.Title),
                                    Duties = a.Duties.OrderBy(o => o.Description).Select(d => d.Description)
                                })
                        })
                })
                .ToListAsync();
            
            return Ok(new {items = employments});
        }
    }

    public class EmploymentDto
    {
        public class EmployerDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public EmployerDto Employer { get; set; }
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IEnumerable<CareerStep> CareerSteps { get; set; }

        public class CareerStep
        {
            public string Title { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public IEnumerable<AssignmentDto> Assignments { get; set; }
        }

        public class AssignmentDto
        {
            public string Description { get; set; }
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Summary { get; set; }
            public IEnumerable<string> Technologies { get; set; }
            public IEnumerable<string> Duties { get; set; }
        }
    }
}