using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace LoadBalancer.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((builderContext, config) =>
                    {
                        config.SetBasePath(builderContext.HostingEnvironment.ContentRootPath);
                        config.AddJsonFile("Config\\config.json", false, true);
                    })
                .UseKestrel()
                .UseStartup<Startup>();
    }
}
