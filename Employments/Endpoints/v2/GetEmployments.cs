using System.Collections.Generic;
using System.Linq;
using BusinessCard.Employments.Services;
using Router;
using Router.Request;
using Router.Response.Extensions;
using Router.Serve;
using Router.Validation.Numbers;

namespace BusinessCard.Employments.Endpoints.v2
{
    public class GetEmployments : IEndpoint<GetEmployments>
    {
        public void Define(IEndpointBuilder<GetEmployments> configure)
        {
            configure.Get("/api/v2/people/{id}/employments")
                .As(data => data.FromUri<int>(a => a.IsAboveZero()))
                .Serve()
                .Into<IEnumerable<Model>>()
                .With<IGetEmployments>()
                .Map
                (
                    map => map.Using<IModelToDtoMapper>()
                        .Do((data, mapper) => data.Result.Select(mapper.Map))
                )
                .Respond200Ok(items => new {items});
        }
    }
}