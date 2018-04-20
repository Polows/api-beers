using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using beersapi.Middleware;
using Microsoft.AspNetCore.Builder;

//namespace beersapi.Extensions
namespace Microsoft.AspNetCore.Builder
{
	public static class IApplicactionBuilderExtension
	{
		public static void UseTimer(this IApplicationBuilder app, Action<TimerMiddlewareOptions> optionsAction = null)
		{
			var options = new TimerMiddlewareOptions();
			optionsAction?.Invoke(options);

			app.UseMiddleware<TimerMiddleware>(options);
		}

		public static void UseMethodOverride(this IApplicationBuilder app)
		{
			app.UseMiddleware<XMethodOverrideMiddleware>();
		}
	}
}
