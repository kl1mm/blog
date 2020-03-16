using System.Net.Http;
using System.Threading.Tasks;
using kli.Blog.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace kli.Blog.Client.Pages
{
    public partial class EditEntry : ComponentBase
    {
        [Inject] protected HttpClient? Client { get; set; }
        [Inject] protected IJSRuntime? JSRuntime { get; set; }
        [Parameter] public int EntryId { get; set; }

        protected EntryModel EntryModel { get; set; } = new EntryModel();
        protected string? Error { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && this.EntryId > 0)
            {
                this.EntryModel = await this.Client.GetJsonAsync<EntryModel>($"api/blog/entry/{this.EntryId}");
                this.StateHasChanged();
            }

            await this.JSRuntime!.InvokeVoidAsync("jsinterop.initEntryEditor");
            await base.OnAfterRenderAsync(firstRender);
        }

        private async void OnSaveAsync()
        {
            this.EntryModel.Content = await this.JSRuntime!.InvokeAsync<string>("jsinterop.getEditorContent");

            var response = await this.Client.PostJsonAsync<HttpResponseMessage>("api/blog/saveentry", this.EntryModel);
            if (!response.IsSuccessStatusCode)
                this.Error = await response.Content.ReadAsStringAsync();

        }
    }
}
