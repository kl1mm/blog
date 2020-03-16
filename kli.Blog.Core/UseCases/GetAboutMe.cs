using kli.Blog.Shared.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace kli.Blog.Core.UseCases
{
    public static class GetAboutMe
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
                    Content = C,
                    Published = new DateTime(2020, 02, 02)
                };

                return Task.FromResult(aboutMe);
            }
        }

        private const string C = "	<h2 id=\"what-i-do\">What I do</h2>" +
            "<p>I’ve worked on web technologies at Microsoft since 2010. I first joined the <a href=\"http://get.asp.net\">ASP.NET</a> team in the MVC 3 era, then spent a few years moving through a range of projects including JavaScript libraries and the new <a href=\"https://azure.microsoft.com/en-gb/overview/preview-portal/\">Azure management portal</a>. Most recently I rejoined the ASP.NET team with a goal to make the platform a sheer joy and delight for JavaScript developers.</p>" +
            "<p>Beside this I have strong interests in other aspects of software technology. I started the <a href=\"http://knockout.js\">Knockout.js</a> project way back in 2010 (<em>before client-side MV* was cool</em>) and am one of the same boring faces that keep showing up as a speaker at the tech conferences.</p>" +
            "<p>Previously I developed .NET software as a contractor/consultant for clients in Bristol and beyond, plus wrote some books for Apress, such as <a href=\"http://www.amazon.com/ASP-NET-Framework-Experts-Voice-NET/dp/1430210079\">Pro ASP.NET MVC Framework</a> and its various sequels.</p>" +
            "<h2 id=\"quick-history\">Quick history</h2>" +
            "<p>I grew up in the mighty steel city of Sheffield, went off to Cambridge University to study a BA and Masters’ in mathematics, then decided to stick around in Cambridge for a few more years. In that time I married my wonderful wife Zoe, and developed web and desktop applications for a few local software companies.</p>";
    }
}
