using kli.Blog.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;

namespace kli.Blog.Client.Components
{
	public partial class Pagination : ComponentBase
	{
		[Parameter]
		public BasePagedModel? PagedData { get; set; }

		[Parameter]
		public Action<int>? PageChanged { get; set; }

		private bool HasPages => PagedData?.TotalRowCount > PagedData?.PageSize;
		private bool IsFirstPage => PagedData?.CurrentPage == 0;
		private bool IsLastPage => PagedData?.CurrentPage == PagedData?.TotalRowCount / PagedData?.PageSize;
		private void PagerButtonClicked(int page) => PageChanged?.Invoke(page);
	}
}
