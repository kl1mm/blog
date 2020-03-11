using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace kli.Blog.Core.Behaviours
{
    internal class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private const int secondsBeforeLogWarning = 5;
        private readonly ILogger<TRequest> logger;

        public LoggingBehaviour(ILogger<TRequest> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            using (this.logger.BeginScope($"{typeof(TRequest).Name}_{Guid.NewGuid()}"))
            {
                this.logger.LogInformation($"started");

                var watch = Stopwatch.StartNew();
                var response = await next().ConfigureAwait(false);
                watch.Stop();

                this.logger.LogInformation($"done: {response} ({TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds):g})");
                if (watch.Elapsed > TimeSpan.FromSeconds(secondsBeforeLogWarning))
                    this.logger.LogWarning($"Long running request");

                return response;
            }
        }
    }
}
