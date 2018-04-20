using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace beersapi.Extensions
{
    public static class IWebHostExtensions
    {
	    public static IWebHost MigrateDbContext<TContext>(this IWebHost webHost, Action<TContext, IServiceProvider> seeder)
		    where TContext : DbContext
	    {
		    using (var scope = webHost.Services.CreateScope())
		    {
			    var services = scope.ServiceProvider;
			    var db = services.GetService<TContext>();
			    seeder(db, services);
		    }
		    return webHost;
	    }
    }
}
