using kli.Blog.Core;
using kli.Blog.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace kli.Blog.API.Controllers
{
    public class AboutController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<EntryModel>> Me()
        {
            var result = await this.Mediator.Send(new AboutMe.Request()).ConfigureAwait(false);
            return this.Ok(result);
        }
    }
}
