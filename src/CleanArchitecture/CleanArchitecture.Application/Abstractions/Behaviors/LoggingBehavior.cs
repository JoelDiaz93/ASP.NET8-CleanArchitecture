using CleanArchitecture.Application.Abstractions.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Abstractions.Behaviors;

public class LoggingBehavoir<TRequest, TResponse>
: IPipelineBehavior<TRequest, TResponse>
where TRequest : IBaseCommand
{
  private readonly ILogger<TRequest> _logger;

  public LoggingBehavoir(ILogger<TRequest> logger)
  {
    _logger = logger;
  }

  public async Task<TResponse> Handle(
  TRequest request,
  RequestHandlerDelegate<TResponse> next,
  CancellationToken cancellationToken
)
  {
    var name = request.GetType().Name;
    try
    {
      _logger.LogInformation($"Ejecutando el command request {name}");
      var result = await next();
      _logger.LogInformation($"El command {name} se ejecuto exitosamente");
      return result;
    }
    catch (Exception exception)
    {
      _logger.LogError(exception, $"El command {name} tuvo errores");
      throw;
    }
  }
}