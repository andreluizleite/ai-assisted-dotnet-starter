using CleanArchitecture.Application.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Api.Errors;

public sealed class ApiExceptionHandler(ILogger<ApiExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (status, title) = exception switch
        {
            ValidationException => (StatusCodes.Status400BadRequest, "Validation failed"),
            ArgumentException => (StatusCodes.Status400BadRequest, "Invalid request"),
            CustomerNotFoundException => (StatusCodes.Status404NotFound, "Customer not found"),
            EmailAlreadyInUseException => (StatusCodes.Status409Conflict, "Email already in use"),
            DbUpdateException => (StatusCodes.Status409Conflict, "Data conflict"),
            _ => (StatusCodes.Status500InternalServerError, "Unexpected error")
        };

        if (status >= StatusCodes.Status500InternalServerError)
        {
            logger.LogError(exception, "Unhandled exception for {Method} {Path}", httpContext.Request.Method, httpContext.Request.Path);
        }

        var extensions = new Dictionary<string, object?>
        {
            ["traceId"] = httpContext.TraceIdentifier
        };

        if (exception is ValidationException validationException)
        {
            extensions["errors"] = validationException.Errors
                .GroupBy(error => error.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(error => error.ErrorMessage).Distinct().ToArray());
        }

        var result = Results.Problem(
            statusCode: status,
            title: title,
            detail: exception.Message,
            extensions: extensions);

        await result.ExecuteAsync(httpContext);
        return true;
    }
}
