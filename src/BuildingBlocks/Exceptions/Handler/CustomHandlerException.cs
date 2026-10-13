using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler;

public class CustomHandlerException 
    (ILogger<CustomHandlerException> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        var correlationId= context.Items["X-Correlation-ID"]?.ToString();
        logger.LogError(
            "Error Message: {exceptionMessage}, Time of Concurrence: {time}"
            ,exception.Message,DateTime.UtcNow);
        logger.LogError(
            exception,
            "Request failed. CorrelationId: {CorrelationId}",
            correlationId);
        (string Detail,string Title,int StatusCode) details = exception switch
        {
            InternalServerException=>
            (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode=StatusCodes.Status500InternalServerError
            ),
            ValidationException=>
            (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode=StatusCodes.Status400BadRequest
            ),
            BadRequestException=>
            (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode=StatusCodes.Status400BadRequest
            ),
            NotFoundException =>
            (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode=StatusCodes.Status404NotFound
            ),
            _ =>
            (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode=StatusCodes.Status500InternalServerError   
            )
        };
       

        var problemDetails= new ProblemDetails
        {
            Title= details.Title,
            Detail=details.Detail,
            Status=details.StatusCode,
            Instance=context.Request.Path,
        };
        problemDetails.Extensions.Add("correlationId",correlationId);
        if(exception is ValidationException validationException)
        {
            var errors= validationException.Errors.Select(error=> new
            {
               error.PropertyName,
               error.ErrorMessage 
            });
            problemDetails.Extensions.Add("Validation Errors", errors);
        }
        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
        return true;
    }
}