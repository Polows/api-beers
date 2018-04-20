using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using beersapi.Data;
using beersapi.Extensions;
using beers_api.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace beers_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
	        BuildWebHost(args)
		        .MigrateDbContext<BeersContext>( (ctx, sp) =>
		        {
			        if (ctx.Beers.Any()) { return; }
			        var damm = new BreweryEntity() {Name = "Damm"};

			        ctx.Beers.Add(new Data.BeerEntity()
			        {
				        Abv = 7.3,
				        Name = "Estrella",
				        Brewery = damm
			        });
			        ctx.SaveChanges();
		        })
		        .Run();
			//Datos


        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
