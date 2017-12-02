using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // var options = new WeatherServiceOptions();
            // Configuration.GetSection("weatherService").Bind(options);

            services.AddMvc();
            services.Configure<WeatherServiceOptions>(Configuration.GetSection("weatherService"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.Run(async ctx => Console.WriteLine($"PATH: {ctx.Request.Path}"));
            // app.Use(async (ctx, next) =>
            // {
            //     Console.WriteLine($"PATH: {ctx.Request.Path}");
            //     await next();
            // });
            app.UseMvc();
        }
    }
}
