using System.Collections.Generic;

namespace kli.Blog.Shared.Models
{
	public class PagedModel<T>
	{
		public int PageSize { get; set; } = 5;
		public int CurrentPage { get; set; } = 0;
		public int TotalRowCount { get; set; } = 0;
		public IEnumerable<T> Rows { get; set; } = new List<T>();
	}
}

