using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using kli.Blog.Shared.Models;
using MediatR;

namespace kli.Blog.Core
{
    public static class GetBlogOverview
    {
        private static readonly IQueryable<EntryModel> fakes = new List<EntryModel>
        {
            new EntryModel {Id=1, Header = "Eintrag Nr. 1", Intro = "Hallo von der Welt verrückt oder", Published = DateTime.Now},
            new EntryModel {Id=2, Header = "Eintrag Nr. 3", Intro = "Hallo von der Welt verrückt oder", Published = DateTime.Now},
            new EntryModel {Id=3, Header = "Eintrag Nr. 2", Intro = "Hallo von der Welt verrückt oder", Published = DateTime.Now},
            new EntryModel {Id=4, Header = "Eintrag Nr. 4", Intro = "Hallo von der Welt verrückt oder", Published = DateTime.Now},
            new EntryModel {Id=5, Header = "Eintrag Nr. 5", Intro = "Hallo von der Welt verrückt oder", Published = DateTime.Now},
            new EntryModel {Id=6, Header = "Eintrag Nr. 6", Intro = "Hallo von der Welt verrückt oder", Published = DateTime.Now},
            new EntryModel {Id=7, Header = "Eintrag Nr. 7", Intro = "Hallo von der Welt verrückt oder", Published = DateTime.Now},
            new EntryModel {Id=8, Header = "Eintrag Nr. 8", Intro = "Hallo von der Welt verrückt oder", Published = DateTime.Now},
            new EntryModel {Id=9, Header = "Eintrag Nr. 9", Intro = "Hallo von der Welt verrückt oder", Published = DateTime.Now},
            new EntryModel {Id=10, Header = "Eintrag Nr. 10", Intro = "Hallo von der Welt verrückt oder", Published = DateTime.Now},
            new EntryModel {Id=11, Header = "Eintrag Nr. 11", Intro = "Hallo von der Welt verrückt oder", Published = DateTime.Now},
            new EntryModel {Id=12, Header = "Eintrag Nr. 12", Intro = "Hallo von der Welt verrückt oder", Published = DateTime.Now},
            new EntryModel {Id=13, Header = "Eintrag Nr. 13", Intro = "Hallo von der Welt verrückt oder", Published = DateTime.Now},
            new EntryModel {Id=14, Header = "Eintrag Nr. 14", Intro = "Hallo von der Welt verrückt oder", Published = DateTime.Now},
        }.AsQueryable();

        public class Request : IRequest<PagedModel<OverviewModel>>
        {
            public int CurrentPage { get; set; }
            public int PageSize { get; set; }
        }

        internal class Handler : IRequestHandler<Request, PagedModel<OverviewModel>>
        {
            public Task<PagedModel<OverviewModel>> Handle(Request request, CancellationToken cancellationToken)
            {
                var model = new PagedModel<OverviewModel>()
                {
                    PageSize = request.PageSize,
                    CurrentPage = request.CurrentPage,
                    TotalRowCount = fakes.Count(),
                    Rows = fakes.OrderByDescending(entry => entry.Published)
                        .Skip(request.CurrentPage * request.PageSize)
                        .Take(request.PageSize)
                        .Select(OverviewModel.Projection)
                        .ToList()
                };

                return Task.FromResult(model);
            }
        }
    }
}
