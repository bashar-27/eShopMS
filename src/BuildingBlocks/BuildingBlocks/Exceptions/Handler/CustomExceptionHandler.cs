using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
//using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler

    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error Message: {exceptionMessage}, Time of occurrence {time}", exception.Message, DateTime.UtcNow);
            (string Detail, string Title, int StatusCode) details = exception switch
            {
                InternalServerEx => (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status500InternalServerError
                ),
                ValidationException => (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                BadRequestEx => (
                exception.Message,
                exception.GetType().Name,

                context.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                NotFoundEx => (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status404NotFound
                ),
                _ => (
                    exception.Message,
                    exception.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError
                )
            };
            var exDetails = new ProblemDetails
            {
                Title = details.Title,
                Detail = details.Detail,
                Status = details.StatusCode,
                Instance = context.Request.Path
            };
            exDetails.Extensions.Add("traceId", context.TraceIdentifier);
            if (exception is ValidationException validatoinException)
            {
                exDetails.Extensions.Add("ValidationErrors", validatoinException.Errors);
            }

            await context.Response.WriteAsJsonAsync(exDetails, cancellationToken: cancellationToken);
            return true;    
        }

    }
}