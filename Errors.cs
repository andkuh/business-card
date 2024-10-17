using System;
using System.Linq;
using System.Net;
using Router.Data.Exceptions;
using Router.ErrorHandling;
using Router.ErrorHandling.Configuration;
using Router.Exceptions;

namespace BusinessCard
{
    public class Errors : IErrorResponse<ErrorResponse, Errors>
    {
        public void Define(IStatusCodeConfig<ErrorResponse, Errors> configure)
        {
            configure[HttpStatusCode.NotFound] = respond =>
                respond.On<RecordNotFoundException>(
                    s => new ErrorResponse()
                    {
                        Message =
                            $"Requested type {s.Type.Name} with identity of {string.Join(", ", s.Ids)} was not found"
                    });

            configure[HttpStatusCode.InternalServerError] = when =>
                when.On<Exception>(s => new ErrorResponse() {Message = "Unknown Error"});

            configure[HttpStatusCode.BadRequest] = respond =>
                respond.On<RequestValidationException>(s => new ErrorResponse() {Message = "Bad request"});
        }
    }
}