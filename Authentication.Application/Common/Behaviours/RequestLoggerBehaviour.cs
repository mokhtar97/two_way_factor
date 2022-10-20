using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Application.Common.Behaviours
{

    public class RequestLoggerBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger<TRequest> logger;

        public RequestLoggerBehaviour(ILogger<TRequest> logger)
        {
            this.logger = logger;
        }
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;
            logger.LogInformation("Skeleton Request: {@Name} {@Request}",
                name, request);

            return Task.CompletedTask;
        }
    }
}
