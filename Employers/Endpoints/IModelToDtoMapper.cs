using BusinessCard.Employers.UseCases;
using BusinessCard.Employers.UseCases.GetEmployments;

namespace BusinessCard.Employers.Endpoints
{
    public interface IModelToDtoMapper
    {
        EmploymentDto Map(Model model);
    }
}