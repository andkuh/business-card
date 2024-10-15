using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessCard.Employments.Services;

namespace BusinessCard.Employments.UseCases.GetEmployments
{
    public interface IGetEmploymentsQueryHandler
    {
        public Task<IEnumerable<Model>> HandleAsync(Query query);
    }
}