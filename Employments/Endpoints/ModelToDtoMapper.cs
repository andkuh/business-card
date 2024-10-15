using System.Linq;
using BusinessCard.Employments.Services;
using BusinessCard.Employments.UseCases.GetEmployments;

namespace BusinessCard.Employments.Endpoints
{
    public class ModelToDtoMapper : IModelToDtoMapper
    {
        public EmploymentDto Map(Model model)
        {
            return new EmploymentDto
            {
                Id = model.Id,
                Employer = new EmploymentDto.EmployerDto
                {
                    Id = model.Employer.Id,
                    Name = model.Employer.Name
                },
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                CareerSteps = model.CareerSteps.Select(jobTitle =>
                    new EmploymentDto.CareerStep
                    {
                        Title = jobTitle.Title,
                        StartDate = jobTitle.StartDate,
                        EndDate = jobTitle.EndDate,
                        Assignments = jobTitle.Assignments
                            .Select(a => new EmploymentDto.AssignmentDto()
                            {
                                Description = a.Description,
                                Link = a.Link != null
                                    ? new EmploymentDto.LinkDto {Address = a.Link.Address, Caption = a.Link.Caption}
                                    : null,
                                Id = a.Id,
                                Name = a.Name,
                                StartDate = a.StartDate < jobTitle.StartDate ? jobTitle.StartDate : a.StartDate,
                                EndDate = a.EndDate > jobTitle.EndDate ? jobTitle.EndDate : a.EndDate,
                                Summary = a.Summary,
                                Technologies = a.Technologies,
                                Duties = a.Duties
                            })
                    })
            };
        }
    }
}