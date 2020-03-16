using System;
using System.Linq.Expressions;

namespace kli.Blog.Shared.Models
{
    public class OverviewModel
    {
        public int Id { get; set; } = 0;
        public string Header { get; set; } = string.Empty;
        public string Intro { get; set; } = string.Empty;
        public bool IsPublished { get; set; }
        public DateTime Published { get; set; }

        public static Expression<Func<EntryModel, OverviewModel>> Projection
            => e => new OverviewModel
            {
                Id = e.Id,
                Header = e.Header,
                Intro = e.Intro,
                IsPublished = e.IsPublished,
                Published = e.Published
            };
    }
}

