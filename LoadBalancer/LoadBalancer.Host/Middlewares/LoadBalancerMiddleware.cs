using System;
using System.Net.Http;
using System.Threading.Tasks;
using LoadBalancer.Host.Counters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace LoadBalancer.Host.Middlewares
{
    public class LoadBalancerMiddleware
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ICounter _counter;
        private readonly Config.Config _config;
        
        public LoadBalancerMiddleware(RequestDelegate next, IHttpClientFactory clientFactory, ICounter counter, IOptions<Config.Config> config)
        {
            _clientFactory = clientFactory;
            _counter = counter;
            _config = config.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var counter = _counter.Next();
            var host = _config.Hosts[counter % _config.Hosts.Count];
            
            var contextRequest = context.Request;
            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri(host);
            var request = new HttpRequestMessage(new HttpMethod(contextRequest.Method), host + contextRequest.Path.ToString());
            var response = await client.SendAsync(request);

            context.Response.StatusCode = (int)response.StatusCode;
            await response.Content.CopyToAsync(context.Response.Body);

            _counter.Reset(int.MaxValue - (_config.Hosts.Count + 1));
        }
    }
}
