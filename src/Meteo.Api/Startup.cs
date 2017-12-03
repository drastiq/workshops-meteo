using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Meteo.Api.Framework;
using Meteo.Core.DI;
using Meteo.Core.EF;
using Meteo.Core.Events;
using Meteo.Core.Mapper;
using Meteo.Core.RabbitMq;
using Meteo.Core.Repositories;
using Meteo.Core.Security;
using Meteo.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Meteo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // var options = new WeatherServiceOptions();
            // Configuration.GetSection("weatherService").Bind(options);

            services.AddMvc()
                .AddXmlSerializerFormatters();
            services.AddLogging();
            services.AddMemoryCache();
            services.Configure<WeatherServiceOptions>(Configuration.GetSection("weatherService"));
            services.Configure<SqlOptions>(Configuration.GetSection("sql"));
            services.AddJwt();
            // services.AddScoped<IWeatherService,WeatherService>();
            // services.AddScoped<ICityService,CityService>();
            // services.AddSingleton<ICityRepository,InMemoryCityRepository>();
            services.AddSingleton<IMapper>(_ => AutoMapperConfig.GetMapper());
            services.AddAuthorization(x => x.AddPolicy("require-admin", p => 
            {
                p.RequireRole("admin");
            }));

            services.AddEntityFrameworkSqlServer()
                    .AddEntityFrameworkInMemoryDatabase()
                    .AddDbContext<MeteoContext>();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule<ServicesModule>();
            builder.AddRabbitMq();
            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment() || env.IsEnvironment("local"))
            {
                app.UseDeveloperExceptionPage();
            }
            loggerFactory.AddConsole().AddDebug();
            //app.Run(async ctx => Console.WriteLine($"PATH: {ctx.Request.Path}"));
            // app.Use(async (ctx, next) =>
            // {
            //     Console.WriteLine($"PATH: {ctx.Request.Path}");
            //     await next();
            // });
            //app.UseExceptionHandler("/error");
            app.UseErrorHandler();
            app.UseRabbitMq()
                .SubscribeEvent<CityAdded>();
            app.UseMvc();
            appLifetime.ApplicationStopped.Register(() => Container.Dispose());
        }
    }
}
