using System.Threading.Tasks;
using kli.Blog.Core.UseCases;
using kli.Blog.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace kli.Blog.API.Controllers
{
    public class BlogController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<PagedModel<OverviewModel>>> Overview(int page, int pageSize = 5)
        {
            var result = await this.Mediator.Send(new GetBlogOverview.Request { CurrentPage = page, PageSize = pageSize });
            return this.Ok(result);
        }

        [HttpGet("{entryId}")]
        public async Task<ActionResult<EntryModel>> Entry(int entryId)
        {
            var result = await this.Mediator.Send(new GetBlogEntry.Request { Id = entryId });
            return this.Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> SaveEntry(SaveBlogEntry.Request request)
        {
            var result = await this.Mediator.Send(request);
            return this.Ok(result);
        }

        [HttpDelete("{entryId}")]
        public async Task<ActionResult> DeleteEntry(int entryId)
        {
            var result = await this.Mediator.Send(new DeleteBlogEntry.Request { Id = entryId });
            return this.Ok(result);
        }
    }
}

