using Microsoft.AspNetCore.Authentication;

namespace beersapi.Auth
{
    public static class AuthenticacionBuilderExtensions
    {
	    public static AuthenticationBuilder AddFakeAuth(this AuthenticationBuilder builder)
	    {
		    builder.AddScheme<FakeAuthOptions, FakeAuthHandler>("Fake", options => { });
		    return builder;
	    }
    }
}
