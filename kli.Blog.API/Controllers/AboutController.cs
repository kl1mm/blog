using kli.Blog.Core.UseCases;
using kli.Blog.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace kli.Blog.API.Controllers
{
    public class AboutController : BaseController
	{
		[HttpGet]
		public async Task<ActionResult<EntryModel>> Me()
		{
			var result = await this.Mediator.Send(new GetAboutMe.Request()).ConfigureAwait(false);
			return this.Ok(result);
		}
	}
}
