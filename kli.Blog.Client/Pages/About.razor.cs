using kli.Blog.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace kli.Blog.Client.Pages
{
	public partial class About : ComponentBase
	{
		[Inject] protected HttpClient? Client { get; set; }

		protected EntryModel? AboutEntry { get; set; }

		protected async override Task OnInitializedAsync()
		{
			this.AboutEntry = await this.Client.GetJsonAsync<EntryModel>("api/About/me") ?? null;
		}
	}
}
