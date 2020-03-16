using System.Threading;
using System.Threading.Tasks;
using kli.Blog.Core.Contracts.Data;
using MediatR;

namespace kli.Blog.Core.UseCases
{
    public static class InitializeApplication
    {
        public class Request : IRequest { }

        internal class Handler : IRequestHandler<Request>
        {
            private readonly IDataStoreInitializer storeInitializer;

            public Handler(IDataStoreInitializer storeInitializer)
            {
                this.storeInitializer = storeInitializer;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                await this.storeInitializer.MigrateAsync().ConfigureAwait(false);
                return Unit.Value;
            }
        }
    }
}
