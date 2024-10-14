using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessCard.Employments.Records;
using BusinessCard.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BusinessCard.Employers.UseCases.GetEmployments
{
    public class GetEmployments : IGetEmployments
    {
        private readonly Ctx _context;

        public GetEmployments(Ctx context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Model>> Get(Query query)
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
                .Where(s => s.PersonId == query.Id)
                .AsNoTracking();

            var employments = await queryable.OrderByDescending(s => s.StartDate)
                .Select(s => new Model
                {
                    Id = s.Id,
                    Employer = new Model.EmployerModel
                    {
                        Id = s.Employer.Id,
                        Name = s.Employer.Name
                    },
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    CareerSteps = s.JobTitles.OrderByDescending(j => j.StartDate).Select(jobTitle =>
                        new Model.CareerStep
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
                                .Select(a => new Model.Assignment()
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

            return employments;
        }
    }
}