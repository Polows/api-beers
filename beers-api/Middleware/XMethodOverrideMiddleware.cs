using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace beersapi.Middleware
{
    public class XMethodOverrideMiddleware
    {
	    private readonly RequestDelegate _next;

	    public XMethodOverrideMiddleware(RequestDelegate next) => _next = next;

	    public async Task Invoke(HttpContext ctx)
	    {
			// Analizar la peticion y modificarla si cabe
		    var request = ctx.Request;
			if (request.Headers.ContainsKey("X-HTTP-Method-Override"))
		    {
			    var header = request.Headers["X-HTTP-Method-Override"];
			    request.Method = header;
		    }
			// Generar una respuesta (si quiero)
		    await _next.Invoke(ctx);
	    }
    }
}
