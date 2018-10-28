using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;

namespace LoadBalancer.Host
{
    public class Startup
    {
        private static readonly IReadOnlyList<string> Hosts = new List<string>
        {
            "http://localhost:5001",
            "http://localhost:5002"
        }.AsReadOnly();

        private static int Counter = 0;

        public void ConfigureServices(IServiceCollection services)
        {
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                Interlocked.Increment(ref Counter);
                var host = Hosts[Counter % Hosts.Count];

                using (var client = new HttpClient())
                {
                    var contextRequest = context.Request;
                    client.BaseAddress = new Uri(host);
                    var request = new HttpRequestMessage(new HttpMethod(contextRequest.Method), contextRequest.Path.ToString());
                    var response = await client.SendAsync(request);

                    context.Response.StatusCode = (int)response.StatusCode;
                    await response.Content.CopyToAsync(context.Response.Body);
                }

                Interlocked.CompareExchange(ref Counter, 0, int.MaxValue - (Hosts.Count + 1));
            });
        }
    }
}
