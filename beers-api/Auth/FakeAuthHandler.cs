using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace beersapi.Auth
{
    public class FakeAuthHandler : AuthenticationHandler<FakeAuthOptions>
    {
	    public FakeAuthHandler(IOptionsMonitor<FakeAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
	    {
	    }

	    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
	    {
		    var request = Context.Request;
		    if (request.Headers.ContainsKey("Authorization"))
		    {
			    var auth = request.Headers["Authorization"].FirstOrDefault();
			    if (!string.IsNullOrEmpty(auth))
			    {
					var claim = new Claim(ClaimTypes.NameIdentifier, auth);
					var identity = new ClaimsIdentity(new[] { claim}, "Fake");
				    var ppal = new ClaimsPrincipal(identity);
					var ticket = new AuthenticationTicket(ppal, "Fake");
				    return AuthenticateResult.Success(ticket);

			    }
		    }
			return AuthenticateResult.NoResult();
	    }
    }
}
