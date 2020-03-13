using kli.Blog.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Net.Http;

namespace kli.Blog.Client.Pages
{
	public partial class SignIn : ComponentBase
	{
		private readonly SignInModel Model = new SignInModel();
		[Inject] protected HttpClient? Client { get; set; }
		[Inject] protected NavigationManager? Navigation { get; set; }

		private async void Submit(EditContext editContext)
		{
			var content = new FormUrlEncodedContent(new[] {
				KeyValuePair.Create(nameof(SignInModel.Login), this.Model.Login),
				KeyValuePair.Create("PasswordHash", this.Model.Password.Sha256()),
			});

			var response = await this.Client!.PostAsync("api/authentication/signin", content);
			if (response.IsSuccessStatusCode)
				this.Navigation!.NavigateTo("/", true);

			this.Model.Error = await response.Content.ReadAsStringAsync();
			this.StateHasChanged();
		}

		private class SignInModel
		{
			public string Login { get; set; } = string.Empty;
			public string Password { get; set; } = string.Empty;
			public string? Error { get; set; }
		}
	}
}
