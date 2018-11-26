using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using LoadBalancer.Host.Counters;
using LoadBalancer.Host.Middlewares;
using Microsoft.Extensions.Configuration;

namespace LoadBalancer.Host
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddSingleton<ICounter, InMemoryCounter>();
            services.Configure<Config.Config>(_configuration.GetSection(nameof(Config.Config)));
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<LoadBalancerMiddleware>();
        }
    }
}
