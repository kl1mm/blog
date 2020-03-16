using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using kli.Blog.Core.Contracts.Data;
using kli.Blog.Core.Entities;
using kli.Blog.Shared.Models;
using MediatR;

namespace kli.Blog.Core.UseCases
{
    public static class GetBlogOverview
    {
        public class Request : IRequest<PagedModel<OverviewModel>>
        {
            public int CurrentPage { get; set; }
            public int PageSize { get; set; }
        }

        internal class Handler : IRequestHandler<Request, PagedModel<OverviewModel>>
        {
            private readonly IUnitOfWork unitOfWork;
            private readonly ClaimsPrincipal user;

            public Handler(IUnitOfWork unitOfWork, ClaimsPrincipal user)
            {
                this.unitOfWork = unitOfWork;
                this.user = user;
            }

            public Task<PagedModel<OverviewModel>> Handle(Request request, CancellationToken cancellationToken)
            {
                Expression<Func<BlogEntry, bool>> publishedEntriesPredicate = entry => this.user.Identity.IsAuthenticated || entry.IsPublished;

                using (var scope = this.unitOfWork.Begin())
                {
                    var model = new PagedModel<OverviewModel>()
                    {
                        PageSize = request.PageSize,
                        CurrentPage = request.CurrentPage,
                        TotalRowCount = scope.SetOf<BlogEntry>().Count(publishedEntriesPredicate),
                        Rows = scope.SetOf<BlogEntry>()
                            .Where(publishedEntriesPredicate)
                            .OrderByDescending(entry => entry.Published)
                            .Skip(request.CurrentPage * request.PageSize)
                            .Take(request.PageSize)
                            .Select(e => new OverviewModel
                            {
                                Id = e.Id,
                                Header = e.Header,
                                Intro = e.Intro,
                                Published = e.Published
                            })
                            .ToList()
                    };
                    return Task.FromResult(model);
                }
            }

            internal class Validator : AbstractValidator<Request>
            {
                public Validator()
                {
                    this.RuleFor(req => req.CurrentPage).GreaterThanOrEqualTo(0);
                    this.RuleFor(req => req.PageSize).GreaterThan(0);
                }
            }
        }
    }
}
