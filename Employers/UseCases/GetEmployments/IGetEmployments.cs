using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessCard.Employers.UseCases.GetEmployments
{
    public interface IGetEmployments
    {
        public Task<IEnumerable<Model>> Get(Query query);
    }
}