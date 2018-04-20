using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using beersapi;
using beersapi.Auth;
using beersapi.Config;
using beersapi.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace beers_api
{
	public class Startup
	{
		private IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			//services.AddTransient<BeersRepository>(); /* Crea un objeto nuevo (Transiet) */

			//services.AddSingleton<IBeersRepository, BeersRepository>(); /* Se injecta el mismo (Singleton) */
			//services.AddSingleton<IBeersRepository>(new BeersRepository()); /* No llama a Dispose */

			//services.AddScoped<BeersRepository>(); /* Singleton detro de la misma peticion */
			services.AddMvc(options =>
				{
					options.InputFormatters.Insert(0, new GuidInputFormatter());
					/* No usar nunca los dos a la vez */
					options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
					//options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
					options.OutputFormatters.Add(new CsvOutputFormatter());
				}
			);

			/* Servicios de autenticacion */
			/* Cookie
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

			}).AddCookie(); 
			*/
			
			/* Token
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

			}).AddJwtBearer();
			*/

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = "Fake";
				options.DefaultChallengeScheme = "Fake";

			}).AddFakeAuth();

			services.AddOptions();
			//services.Configure<AppSettings>(Configuration);
			services.Configure<BeersSettings>(Configuration.GetSection("beers"));

			services.AddDbContext<BeersContext>(options =>
			{
				options.UseSqlServer(Configuration["constr"]);
				options.ConfigureWarnings(w => w.Throw(RelationalEventId.QueryClientEvaluationWarning)); /* Sentencias LINQ que no se puede traducir a SQL */
			});

			// Usar AutoFac para registrar elementos.
			var builder = new ContainerBuilder();

			//Se queda en memoria (los datos estan en memoria)
			//builder.RegisterType<BeersRepository>().As<IBeersRepository>().SingleInstance();
			//Se crea y se destruye (los datos estan en db)
			builder.RegisterType<BeersRepository>().As<IBeersRepository>();

			builder.Populate(services);
			var container = builder.Build();
			return new AutofacServiceProvider(container);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			//app.UseMiddleware<TimerMiddleware>();
			//app.UseTimer();
			app.UseTimer(options =>
			{
				options.SetDefaultMessage("Time elapsed");
			});

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			/*
			app.Map("/api", map =>
			{
				map.UseMvc();
			});
			*/

			//app.Run(async (context) =>
			//{
			//	await context.Response.WriteAsync("Hello World!");
			//});

			//app.UseMvc(rb =>
			//	{
			//		rb.MapRoute("default", "{controller}/{action}", new { action = "GetAll" });
			//	}
			//);

			/*
			Anterior
			app.UseCookieAuthentication();
			app.UseJwtBearerAuthentication();
			*/
			/* Nuevo */
			app.UseAuthentication();

			//app.UseMiddleware<XMethodOverrideMiddleware>();
			app.UseMethodOverride();
			app.UseMvc();
		}
	}
}
