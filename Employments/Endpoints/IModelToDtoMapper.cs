using BusinessCard.Employments.Services;
using BusinessCard.Employments.UseCases.GetEmployments;

namespace BusinessCard.Employments.Endpoints
{
    public interface IModelToDtoMapper
    {
        EmploymentDto Map(Model model);
    }
}