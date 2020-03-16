using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using kli.Blog.Core.Settings;
using MediatR;
using Microsoft.Extensions.Options;

namespace kli.Blog.Core.UseCases
{
    public static class AuthenticateUser
    {
        public class Request : IRequest<ClaimsPrincipal>
        {
            public string Login { get; set; } = string.Empty;
            public string PasswordHash { get; set; } = string.Empty;
            public string Scheme { get; set; } = string.Empty;
        }

        internal class Handler : IRequestHandler<Request, ClaimsPrincipal>
        {
            private readonly AppSettings appSettings;

            public Handler(IOptions<AppSettings> appSettingsAccessor)
            {
                this.appSettings = appSettingsAccessor.Value;
            }

            public Task<ClaimsPrincipal> Handle(Request request, CancellationToken cancellationToken)
            {
                var identity = new ClaimsIdentity();
                if (this.ValidateCrendentials(request))
                    identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "mMilk") }, request.Scheme);

                return Task.FromResult(new ClaimsPrincipal(identity));
            }

            private bool ValidateCrendentials(Request request)
            {
                var authSettings = this.appSettings.Authentication;
                return authSettings.Login.Equals(request.Login)
                    && authSettings.PasswordHash.Equals(request.PasswordHash);
            }
        }
    }
}
