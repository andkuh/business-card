using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessCard.Employments.Services;

namespace BusinessCard.Employments.UseCases.GetEmployments
{
    public class GetEmploymentsQueryHandler : IGetEmploymentsQueryHandler
    {
        private readonly IGetEmployments _getEmployments;

        public GetEmploymentsQueryHandler(IGetEmployments getEmployments)
        {
            _getEmployments = getEmployments;
        }
        public Task<IEnumerable<Model>> HandleAsync(Query query)
        {
            return _getEmployments.ServeAsync(query.Id);
        }
    }
}