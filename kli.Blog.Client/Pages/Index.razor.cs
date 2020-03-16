using System.Net.Http;
using System.Threading.Tasks;
using kli.Blog.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace kli.Blog.Client.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject] protected HttpClient? Client { get; set; }
        [Inject] protected NavigationManager? NavigationManager { get; set; }

        protected PagedModel<OverviewModel>? Overview { get; set; }

        protected override async Task OnInitializedAsync() => await this.LoadDataAsync(0);

        private async void OnPageChanged(int page) => await this.LoadDataAsync(page);

        private async Task LoadDataAsync(int page)
        {
            this.Overview = await this.Client!.GetJsonAsync<PagedModel<OverviewModel>>($"api/blog/overview?page={page}");
            this.StateHasChanged();
        }

        private void OnEdit(int entryId) => this.NavigationManager!.NavigateTo($"/edit/{entryId}");

        private async void OnDelete(int entryId)
        {
            await this.Client!.DeleteAsync($"api/blog/deleteEntry/{entryId}");
            await this.LoadDataAsync(this.Overview!.CurrentPage);
        }
    }
}
