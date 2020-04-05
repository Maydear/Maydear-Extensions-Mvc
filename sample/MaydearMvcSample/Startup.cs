using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maydear.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MaydearMvcSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            corsPolicyName = Configuration.GetSection("Cors:PolicyName")?.Value;
        }

        private readonly string corsPolicyName;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMaydearMvc();
            services.AddMaydearRedisCache(Configuration);

            var origins = Configuration.GetSection("Cors:Origins")?.Value;

            if (origins != null && origins.Any())
            {
                services.AddWildcardCors(options =>
                {
                    options.AddPolicy(corsPolicyName, builder => builder.WithOrigins());
                });
            }

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (!string.IsNullOrWhiteSpace(corsPolicyName))
            {
                app.UseCors(corsPolicyName);
            }
            app.UseMaydearMvc();
        }
    }
}
