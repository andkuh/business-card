using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessCard.Employments.Records;
using BusinessCard.Employments.Services;
using BusinessCard.Infrastructure;
using Microsoft.EntityFrameworkCore;

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