using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using kli.Blog.Core.Contracts.Data;
using kli.Blog.Core.Entities;
using MediatR;

namespace kli.Blog.Core.UseCases
{
    public class SaveBlogEntry
    {
        public class Request : IRequest<int>
        {
            public int Id { get; set; } = 0;
            public string Header { get; set; } = string.Empty;
            public string Intro { get; set; } = string.Empty;
            public string Content { get; set; } = string.Empty;
            public bool IsPublished { get; set; }
        }

        internal class Handler : IRequestHandler<Request, int>
        {
            private readonly IUnitOfWork unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                this.unitOfWork = unitOfWork;
            }

            public async Task<int> Handle(Request request, CancellationToken cancellationToken)
            {
                var blogEntry = new BlogEntry
                {
                    Id = request.Id,
                    Header = request.Header,
                    Intro = request.Intro,
                    Content = request.Content,
                    IsPublished = request.IsPublished,
                    Published = request.IsPublished ? DateTime.Now : DateTime.MinValue
                };

                using (var scope = this.unitOfWork.BeginWrite())
                {
                    scope.SetOf<BlogEntry>().InsertOrUpdate(blogEntry);
                    await scope.CompleteAsync();
                }
                return 0;
            }
        }

        internal class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                this.RuleFor(m => m.Header).NotEmpty();
                this.RuleFor(m => m.Intro).NotEmpty();
                this.RuleFor(m => m.Content).NotEmpty();
            }
        }
    }
}
