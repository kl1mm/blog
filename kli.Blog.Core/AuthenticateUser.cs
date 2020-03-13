using MediatR;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace kli.Blog.Core
{
	public static class AuthenticateUser
	{
		public class Request : IRequest<ClaimsPrincipal>
		{
			public string Login { get; set; } = string.Empty;
			public string Password { get; set; } = string.Empty;
		}

		internal class Handler : IRequestHandler<Request, ClaimsPrincipal>
		{
			public Task<ClaimsPrincipal> Handle(Request request, CancellationToken cancellationToken)
			{
				var identity = new ClaimsIdentity();
				if (request.Login == request.Password)
					identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "tobi") }, "Fake authentication type");

				return Task.FromResult(new ClaimsPrincipal(identity));
			}
		}
	}
}
