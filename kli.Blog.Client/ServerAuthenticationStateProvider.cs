using kli.Blog.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace kli.Blog.Client
{
	internal class ServerAuthenticationStateProvider : AuthenticationStateProvider
	{
		private readonly HttpClient httpClient;

		public ServerAuthenticationStateProvider(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async override Task<AuthenticationState> GetAuthenticationStateAsync()
		{
            var user = await this.httpClient.GetJsonAsync<UserModel>("api/authentication/userInfo");

            var identity = user.IsAuthenticated
                ? new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Name) }, nameof(ServerAuthenticationStateProvider))
                : new ClaimsIdentity();

			return new AuthenticationState(new ClaimsPrincipal(identity));
		}
	}
}
