using kli.Blog.Core.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace kli.Blog.Core
{
    public class AboutMe
    {
        public class Request : IRequest<EntryModel>
        { }

        internal class Handler : IRequestHandler<Request, EntryModel>
        {
            public Task<EntryModel> Handle(Request request, CancellationToken cancellationToken)
            {
                var aboutMe = new EntryModel
                {
                    Header = "About me",
                    Intro = "Software Engineer, Sports junky, Husband living and working in Hamburg, Germany",
                    Content = "some content",
                    Published = new DateTime(2020, 02, 02)
                };

                return Task.FromResult(aboutMe);
            }
        }
    }
}
