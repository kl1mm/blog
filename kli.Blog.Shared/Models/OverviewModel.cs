using System;
using System.Linq.Expressions;

namespace kli.Blog.Shared.Models
{
	public class OverviewModel
	{
		public string Header { get; set; } = string.Empty;
		public string Intro { get; set; } = string.Empty;
		public DateTime Published { get; set; }

		public static Expression<Func<EntryModel, OverviewModel>> Projection
			=> e => new OverviewModel { Header = e.Header, Intro = e.Intro, Published = e.Published };

	}
}

