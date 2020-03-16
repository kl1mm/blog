using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using kli.Blog.Core.Contracts.Data;
using kli.Blog.Core.Entities;
using kli.Blog.Shared.Models;
using MediatR;

namespace kli.Blog.Core.UseCases
{
    public static class GetBlogEntry
    {
        public class Request : IRequest<EntryModel>
        {
            public int Id { get; set; }
        }

        internal class Handler : IRequestHandler<Request, EntryModel>
        {
            private readonly IUnitOfWork unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                this.unitOfWork = unitOfWork;
            }

            public Task<EntryModel> Handle(Request request, CancellationToken cancellationToken)
            {
                using (var scope = this.unitOfWork.Begin())
                {
                    var model = scope.SetOf<BlogEntry>()
                        .Where(entry => entry.Id == request.Id)
                        .Select(e => new EntryModel
                        {
                            Id = e.Id,
                            Header = e.Header,
                            Intro = e.Intro,
                            Content = e.Content,
                            IsPublished = e.IsPublished,
                            Published = e.Published
                        })
                        .SingleOrDefault();

                    return Task.FromResult(model);
                }
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
