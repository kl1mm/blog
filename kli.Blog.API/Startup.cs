using kli.Blog.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace kli.Blog.API
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCore();
			services.AddControllers();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseWebAssemblyDebugging();
			}

			app.UseStaticFiles();
			app.UseBlazorFrameworkFiles();
			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
				endpoints.MapFallbackToFile("index.html");
			});
		}
	}
}
