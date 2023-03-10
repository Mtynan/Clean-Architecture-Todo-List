using Domain.Entities;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.PipelineBehaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
        where TResponse : Response
    {
        private readonly ILogger<IPipelineBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<IPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting request {@RequestName}, {@DatetimeNow}", typeof(TRequest).Name, DateTime.Now);

            var result = await next();
            if (!result.IsSuccess)
            {
                _logger.LogError("Request failure ${}, {@DatetimeNow}, {@Error}", typeof(TRequest).Name, DateTime.Now, result.Error);
            }
            _logger.LogInformation("Completed request {@RequestName}, {@DatetimeNow}", typeof(TRequest).Name, DateTime.Now);
            return result;
        }
    }
}
