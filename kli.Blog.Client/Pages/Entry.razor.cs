using kli.Blog.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace kli.Blog.Client.Pages
{
	public partial class Entry : ComponentBase
	{
		[Inject] protected HttpClient? Client { get; set; }

		[Parameter]
		public int EntryId { get; set; } = 0;

		protected EntryModel? EntryModel { get; set; }

		protected  override async Task OnParametersSetAsync()
		{
			this.EntryModel = await this.Client.GetJsonAsync<EntryModel>($"api/blog/entry/{this.EntryId}");
		}
	}
}
