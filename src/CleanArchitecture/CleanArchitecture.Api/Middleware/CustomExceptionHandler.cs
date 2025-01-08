using CleanArchitecture.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Middleware;

public class CustomExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
  private readonly RequestDelegate _next;
  private readonly ILogger<ExceptionHandlingMiddleware> _logger;

  public async ValueTask<bool> TryHandleAsync(
    HttpContext httpContext,
    Exception exception,
    CancellationToken cancellationToken)
  {
    await _next(httpContext);
    var problemDetails = new ProblemDetails
    {
      Status = exception switch
      {
        ArgumentException => StatusCodes.Status400BadRequest,
        _ => StatusCodes.Status500InternalServerError
      },
      Title = "An error occurred",
      Type = exception.GetType().Name,
      Detail = exception.Message
    };

    return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
    {
      Exception = exception,
      HttpContext = httpContext,
      ProblemDetails = problemDetails
    });
  }

  private static ExceptionDetails GetExceptionDetails(Exception exception)
  {
    return exception switch
    {
      ValidationException validationException => new ExceptionDetails(
          StatusCodes.Status400BadRequest,
          "ValidationFailure",
          "Validacion de Error",
          "han ocurrido uno o mas errores de validacion",
          validationException.Errors
      ),
      _ => new ExceptionDetails(
          StatusCodes.Status500InternalServerError,
          "ServerError",
          "Error de Servidor",
          "Un inesperado error a ocurrido en la App",
          null
      )

    };
  }

  internal record ExceptionDetails(
  int Status,
  string Type,
  string Title,
  string Detail,
  IEnumerable<object>? Errors
);

}