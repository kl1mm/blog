using kli.Blog.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace kli.Blog.Client.Pages
{
	public partial class Index : ComponentBase
	{
		[Inject] protected HttpClient? Client { get; set; }

		protected PagedModel<OverviewModel>? Overview { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await this.LoadDataAsync(0);
		}

		private async void OnPageChanged(int page)
		{
			await LoadDataAsync(page);
		}

		private async Task LoadDataAsync(int page)
		{
			this.Overview = await this.Client
				.PostJsonAsync<PagedModel<OverviewModel>>("api/blog/overview", new { PageSize = 5, CurrentPage = page });
			
			this.StateHasChanged();
		}
	}
}
