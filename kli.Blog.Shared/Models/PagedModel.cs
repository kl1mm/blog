using System.Collections.Generic;

namespace kli.Blog.Shared.Models
{
	public class BasePagedModel
	{
		public int PageSize { get; set; } = 5;
		public int CurrentPage { get; set; } = 0;
		public int TotalRowCount { get; set; } = 0;
	}

	public class PagedModel<T> : BasePagedModel
	{
		public IEnumerable<T> Rows { get; set; } = new List<T>();
	}
}
