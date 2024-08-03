using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails();
        problemDetails.Instance = httpContext.Request.Path;

        switch (exception)
        {
            case FluentValidation.ValidationException fluentException:
                problemDetails.Title = "one or more validation errors occurred - Global Exception Handler.";
                problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                var validationErrors = fluentException.Errors.Select(error => error.ErrorMessage).ToList();
                problemDetails.Extensions.Add("errors", validationErrors);
                break;

            case ProductNotFoundException notFoundException:
                problemDetails.Title = exception.Message;
                problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                break;

            default:
                problemDetails.Title = exception.Message;
                break;
        }

        //if (exception is FluentValidation.ValidationException fluentException)
        //{
        //    problemDetails.Title = "one or more validation errors occurred - Global Exception Handler.";
        //    problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
        //    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        //    List<string> validationErrors = new List<string>();
        //    foreach (var error in fluentException.Errors)
        //    {
        //        validationErrors.Add(error.ErrorMessage);
        //    }
        //    problemDetails.Extensions.Add("errors", validationErrors);
        //}
        //else
        //{
        //    problemDetails.Title = exception.Message;
        //}

        _logger.LogError("{ProblemDetailsTitle}", problemDetails.Title);

        problemDetails.Status = httpContext.Response.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
        return true;
    }
}