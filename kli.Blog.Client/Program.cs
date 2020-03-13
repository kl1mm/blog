using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace kli.Blog.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("app");

			builder.Services.AddBaseAddressHttpClient();
			builder.Services.AddOptions();
			builder.Services.AddAuthorizationCore();
			builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

			await builder.Build().RunAsync();
		}
	}
}
