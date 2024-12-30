using ErrorOr;
using MediatR;
using SavePlan.API.Common;
using Serilog.Context;

namespace SavePlan.API.Behaviors;

public sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : SavePlan.API.Common.IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting request: {@RequestName}, at {@DateTime}",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        var result = await next();

        if (result.IsError)
        {
            using (LogContext.PushProperty("Error", result.Errors!.First(), true))
            {
                _logger.LogError("Request {@RequestName} completed with errors. At {@DateTime}. ",
                    typeof(TRequest).Name,
                    DateTime.UtcNow);
            }
        } 
        else
        {
            _logger.LogInformation("Completed request successfully: {@RequestName}. At {@DateTime}",
                typeof(TRequest).Name,
                DateTime.UtcNow);
        }

        return result;
    }
}
