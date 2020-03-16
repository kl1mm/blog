using System.Threading.Tasks;
using kli.Blog.Core.UseCases;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace kli.Blog.API
{
	public class Program
	{
        public async static Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .Build();

            using (var scope = host.Services.CreateScope())
                await scope.ServiceProvider.GetService<IMediator>().Send(new InitializeApplication.Request());

            await host.RunAsync();
        }
	}
}
