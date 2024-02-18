using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Marvel.API.Exceptions;

namespace Marvel.API.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var statusCode = (int)HttpStatusCode.InternalServerError;

            if (exception is NotFoundException)
            {
                statusCode = (int)HttpStatusCode.NotFound;
            }
            else if (exception is MaximumFavoriteCharacterException)
            {
                statusCode = (int)HttpStatusCode.Conflict;
            }
            else if (exception is AlreadyFavoriteCharacterException)
            {
                statusCode = (int)HttpStatusCode.Conflict;
            }


            var result = new ObjectResult(new
            {
                StatusCode = statusCode,
                Message = exception.Message
            })
            {
                StatusCode = statusCode
            };

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
