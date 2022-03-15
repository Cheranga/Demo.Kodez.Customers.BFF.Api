using Demo.Kodez.Customers.BFF.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Demo.Kodez.Customers.BFF.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    var configuration = builder.Build();
                    builder.RegisterAzureAppConfigurationProviders(context, configuration);
                })
                .ConfigureWebHostDefaults( webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}