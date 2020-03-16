using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace kli.Blog.Core.UseCases
{
    public static class DeleteBlogEntry
    {
        public class Request : IRequest
        {
            public int Id { get; set; } = 0;
        }

        internal class Handler : IRequestHandler<Request>
        {
            public Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                return Unit.Task;
            }
        }
    }
}
