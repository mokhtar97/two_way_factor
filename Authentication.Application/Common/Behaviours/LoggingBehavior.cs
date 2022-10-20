using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var typeName = typeof(TRequest).Name;
            try
            {
                _logger.LogInformation("----- Handling Hoppa For command {CommandName} ({@Command})", typeName, request);
                var response = await next();
                _logger.LogInformation("----- Command {CommandName} handled - response: {@Response}", typeName, response);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Handling Hoppa for {CommandName} ({@Command})", typeName, request);
                throw;
            }

        }
    }
}
