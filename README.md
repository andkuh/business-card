
# Business Card

It appeared not very fun to compose my CV by hands, so I created this little repo and it made several things for me: 
- trained a bit my React skills
- tried in practice my pet DotExpress (or Router, not sure for naming currently) library 
- demostrated a very basic example of how it can be used.
- and.. cherry on a top - CV itself in [online](https://andrei-kukharchuk.tech/). it is not possible to export it to pdf currently, but browser's 'Print..' feature is still able to do it for you.

## Examples

Here is code of [this](https://github.com/andkuh/business-card/blob/master/People/Endpoints/v2/GetPersonTechnologies.cs) endpoint to explain how it works.
```CSharp
public class GetPersonTechnologies : IEndpoint<GetPersonTechnologies>
{
        public void Define(IEndpointBuilder<GetPersonTechnologies> configure)
        {
            configure
                .Get("/api/v2/people/{id}/technologies") // define path
                .As(data => data.FromUri<int>(a => a.IsAboveZero())) // define input contract
                .UseData() // use EF Core data access 
                .SetOf<Assignment>() // define set to operate on
                .Select // define query
                (
                    (assignments, id) => assignments
                        .Include(s => s.Employment)
                        .Include(s => s.Technologies)
                        .AsSplitQuery()
                        .Where(s => s.Employment.PersonId == id)
                        .ToListAsync()
                )
                .MapResult // map result
                (
                    result => result
                        .SelectMany(s => s.Technologies, (assignment, technology) => new
                        {
                            technology, assignment.Employment.Type
                        })
                        .GroupBy(s => s.technology.Title, s => s.Type, (title, types) => new
                        {
                            title,
                            levels = types.Distinct()
                        })
                        .OrderBy(s => s.title)
                        .ToList()
                )
                .Cache(cache => // using in memory cache
                {
                    cache.As(id => CacheKey.For("person", "technologies", ("id", id))) // define cache key
                        .For(30.Minutes()) // define cache lifetime for 30 minutes;
                })
                .Respond200Ok(items => new {items}); // respond with 200 OK and anonymous object with items from MapResult method
        }
    }
```

The same could also be archieved in another way:
```CSharp

public class GetPersonTechnologies : IEndpoint<GetPersonTechnologies>
{
        public void Define(IEndpointBuilder<GetPersonTechnologies> configure)
        {
            configure
                .Get("/api/v2/people/{id}/technologies")
                .As(data => new // using anonymous type instead of primitive
                {
                    id = data.FromUri<int>(a => a.IsAboveZero())
                })
                .UseData() // use data 
                .SetOf<Assignment>()
                .Select // define query
                (
                    (assignments, request) => assignments
                        .Include(s => s.Employment)
                        .Include(s => s.Technologies)
                        .AsSplitQuery()
                        .Where(s => s.Employment.PersonId == request.id) // here we have access to dto object
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
                            levels = types.Distinct()
                        })
                        .OrderBy(s => s.title)
                        .ToList()
                )
                .Cache(cache =>
                {
                    cache.As(id => CacheKey.For("person", "technologies", ("id", id)))
                        .For(30.Minutes());
                })
                .Respond200Ok(items => new {items});
        }
    }
```
