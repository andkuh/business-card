
# Business Card

It appeared not very fun to compose my CV by hands, so I created this little repo and it made several things for me: 
- trained a bit my React skills
- tried in practice my pet DotExpress (or Router, not sure for naming currently) library 
- demostrated a very basic example of how it can be used.
- and.. cherry on a top - CV itself in [online](https://andrei-kukharchuk.tech/). it is not possible to export it to pdf currently, but browser's 'Print..' feature is still able to do it for you.

## DotExpress

As said this application is built using DotExpress library.

If you're curious about what my DotExpress is, here are some examples. In short, the main idea is to have alternative to classic controllers, allowing to implement Web Api endpoints easily and extremely fast without any ceremonies as complex arhitectural patterns ofter do. Based on builder pattern, it brings it's advantages to the API development: considered design of builder elements literally suggests to developer how to use it correctly and combined with human-readable method naming makes development more storytelling alike rather that coding and even non-technical mates may become able to understand what particular endpoint does. 

You could think that it looks Microsoft's MinimalApi alike, but it is not, and it is not built based on it. The idea born before MinimalApi was released, so it went in it's own way. 
It uses builder pattern to configure endpoints, which after a bit of magic under the hood ends up with adult best-practice dependency registrations in DI container, so each endpoint path becomes assosiated with particular ```IEndpointService<TEndpoint>``` implementation which may resolve pretty complex tree of generic objects from ```IServiceProvider``` to handle the http request: to read request, process it and make a response.
DotExpress allows to extend it's functionality: using Scrutor library allowing to register decorators for services opens a huge field for developing extensions, and things like caching, event broadcasting are implemented based on it.
It may sound odd, but you don't event need Dto objects to describe your contracts, you may use anonymous objects without need to implement classes spread among many .cs files. Don't be afraid: separate package is able to document the api and give to you and your users beloved Swagger / OpenApi specification.

The library is not in open source currently, it is only supposed to be, some actions still required for it. Anyway no plans for any promotion or commercial distribution. I'm doing it by myself and for myself, so feel free to like it or dislike it :)

So in the end it's one more non-silver bullet in a world of dev, there is still reasonable question if this approach suitable fot complex applications and systems. Probably complex cases may require more complex arhitectural patterns. But anyway this repo is example of how covered my needs in practice.



## Examples

Here is code of [this](https://github.com/andkuh/business-card/blob/master/People/Endpoints/v2/GetPersonTechnologies.cs) endpoint to explain how it works in practice.
```CSharp
public class GetPersonTechnologies : IEndpoint<GetPersonTechnologies>
{
        public void Define(IEndpointBuilder<GetPersonTechnologies> configure)
        {
            configure
                .Get("/api/v2/people/{id}/technologies") // define path
                .As(data => data.FromUri<int>(id => id.IsAboveZero())) // define input contract
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
                    id = data.FromUri<int>(id => id.IsAboveZero())
                })
                .UseData()
                .SetOf<Assignment>()
                .Select
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

Here is example of using service 

```CSharp
    public class GetEmployments : IEndpoint<GetEmployments>
    {
        public void Define(IEndpointBuilder<GetEmployments> configure)
        {
            configure.Get("/api/v2/people/{id}/employments")
                .As(data => data.FromUri<int>(a => a.IsAboveZero()))
                .Serve() // configure service to use
                .Into<IEnumerable<Model>>()
                .With<IGetEmployments>() // IGetEmployment implement IService<int, IEnumerable<Model>>
                .Map
                (
                    map => map.Using<IModelToDtoMapper>() // configure to use mapper not implementing native IMapper<TIn, TOut> interface
                        .Do((data, mapper) => data.Result.Select(mapper.Map)) // configures how to actually call it
                )
                .Respond200Ok(items => new {items});
        }
    }
```
This is actually basic approach and previous ```.UseData().SetOF<T>().Select()``` does something like following under the hood:
```CSharp
.Serve()
.Into<IEnumerable<Assignment>>()
.With<ISelectService<GetPersonTechnologies, int, Assignment>> // implementing IService<int, IEnumerable<Assignment>>
```

to be appended..
