using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using kli.Blog.Core.Contracts.Data;
using kli.Blog.Core.Entities;
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
            private readonly IUnitOfWork unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                this.unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                using (var scope = this.unitOfWork.BeginWrite())
                {
                    scope.SetOf<BlogEntry>().Delete(new BlogEntry { Id = request.Id });
                    await scope.CompleteAsync();
                }
                return Unit.Value;
            }
        }

        internal class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                this.RuleFor(req => req.Id).GreaterThan(0);
            }
        }
    }
}
