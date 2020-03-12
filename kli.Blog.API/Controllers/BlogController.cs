using kli.Blog.Core;
using kli.Blog.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace kli.Blog.API.Controllers
{
	public class BlogController : BaseController
	{
		[HttpPost]
		public async Task<ActionResult<OverviewModel>> Overview(GetOverview.Request request)
		{
			var result = await this.Mediator.Send(request);
			return this.Ok(result);
		}
	}
}

