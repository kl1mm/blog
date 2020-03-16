using kli.Blog.Shared.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace kli.Blog.Core.UseCases
{
    public static class GetBlogEntry
    {
        public class Request : IRequest<EntryModel>
        {
            public int Id { get; set; }
        }

        internal class Handler : IRequestHandler<Request, EntryModel>
        {
            public Task<EntryModel> Handle(Request request, CancellationToken cancellationToken)
            {
                var model = new EntryModel
                {
                    Header = "Ich bin die Überschrift",
                    Intro = "Ich beschreibe den ganzen Bums hier schon mal ein wenig. Allerding nutze ich kaum Details.",
                    Published = DateTime.Now,
                    Content = A
                };

                return Task.FromResult(model);
            }
        }

        public const string A = "<p>Almost all rich client-side web apps (SPAs) involve interacting with a data store. Normally, that data store is held on some server, and the browser-based app queries it by making HTTP calls to some API endpoint. Another option, though, is to store a database <em>client-side</em> in the browser. The great benefit of doing so is that it permits completely instant querying, and can even work offline.</p>" +
            "<p><a href=\"https://developer.mozilla.org/en-US/docs/Web/API/IndexedDB_API\">IndexedDB</a> has been around for ages, and remains the dominant way to put a client-side database in your SPA. It’s an indexed store of JSON objects, which lets you configure your own versioned data schema and perform efficient queries against the indexes you’ve defined. Naturally, it works both offline and online.</p>" +
            "<h2 id=\"using-indexeddb-with-blazor\">Using IndexedDB with Blazor</h2>" +
            "<p>You <em>could</em> use the native IndexedDB APIs through Blazor’s JS interop capability. But you’ll have a rough time, because - to put it kindly - the IndexedDB APIs are atrocious. IndexedDB came onto the scene before <code class=\"language-plaintext highlighter-rouge\">Promise</code>, so it has an events-based asynchrony model, which is a disaster to work with.</p>" +
            "<p>So, I was pretty intrigued when I heard about <a href=\"https://github.com/Reshiru/Blazor.IndexedDB.Framework\"><code class=\"language-plaintext highlighter-rouge\">Reshiru.Blazor.IndexedDB.Framework</code></a>, a NuGet package described as:</p>";

    }
}
