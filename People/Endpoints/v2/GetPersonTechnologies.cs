using System.Linq;
using BusinessCard.Employments.Records;
using Microsoft.EntityFrameworkCore;
using Router;
using Router.Cache;
using Router.Data;
using Router.Helpers;
using Router.Request;
using Router.Response.Extensions;
using Router.Validation.Numbers;

namespace BusinessCard.People.Endpoints.v2
{
    public class GetPersonTechnologies : IEndpoint<GetPersonTechnologies>
    {
        public void Define(IEndpointBuilder<GetPersonTechnologies> configure)
        {
            configure.Get("/api/v2/people/{id}/technologies")
                .As(data => data.FromUri<int>(a => a.IsAboveZero()))
                .UseData()
                .SetOf<Assignment>()
                .Select
                (
                    (assignments, id) => assignments
                        .Include(s => s.Employment)
                        .Include(s => s.Technologies)
                        .AsSplitQuery()
                        .Where(s => s.Employment.PersonId == id)
                        .ToListAsync()
                )
                .MapResult
                (
                    result => result
                        .SelectMany(s => s.Technologies, (assignment, technology) => new
                        {
                            technology, assignment.Employment.Type
                        })
                        .GroupBy(s => s.technology.Title, s => s.Type, (title, types) => new
                        {
                            title,
                            level = types.Distinct().Min()
                        })
                        .OrderBy(s => s.level).ThenBy(s => s.title)
                        .ToList()
                )
                .Cache(cache => cache.As(id => CacheKey.For("person", "technologies", ("id", id))).For(30.Minutes()))
                .Respond200Ok(items => new {items});
        }
    }
}