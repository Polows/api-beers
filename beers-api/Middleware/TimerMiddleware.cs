using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace beersapi.Middleware
{
	public class TimerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly TimerMiddlewareOptions _options;

		public TimerMiddleware(RequestDelegate next, TimerMiddlewareOptions options)
		{
			_next = next;
			_options = options;
		}

		public async Task Invoke(HttpContext ctx)
		{
			var watch = new Stopwatch();
			watch.Start();
			await _next.Invoke(ctx);
			watch.Stop();
			var ms = watch.ElapsedMilliseconds;
			Debug.WriteLine($"{_options.Text} {ms}");
		}
	}
}
