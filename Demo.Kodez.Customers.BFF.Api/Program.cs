using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Azure.Identity;

namespace Demo.Kodez.Customers.BFF.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    webBuilder.ConfigureAppConfiguration(builder =>
                    {
                        var config = builder.Build();
                        var appConfigString = config["AzureAppConfigurationUrl"];
                        var customersBffConfigUrl = config["CustomersBFFAzureAppConfigurationUrl"];

                        builder.AddAzureAppConfiguration(options =>
                        {
                            var credentials = new DefaultAzureCredential();

                            options.Connect(new Uri(appConfigString), credentials)
                                .ConfigureKeyVault(vaultOptions =>
                                {
                                    vaultOptions.SetCredential(credentials);
                                })
                                .ConfigureRefresh(refreshOptions =>
                                {
                                    refreshOptions.Register("RefreshAll", true).SetCacheExpiration(TimeSpan.FromSeconds(5));
                                })
                                .UseFeatureFlags(flagOptions =>
                                {
                                    flagOptions.CacheExpirationInterval = TimeSpan.FromSeconds(5);
                                });
                        }).AddAzureAppConfiguration(options =>
                        {
                            var credentials = new DefaultAzureCredential();

                            options.Connect(new Uri(customersBffConfigUrl), credentials)
                                .ConfigureKeyVault(vaultOptions =>
                                {
                                    vaultOptions.SetCredential(credentials);
                                })
                                .ConfigureRefresh(refreshOptions =>
                                {
                                    refreshOptions.Register("RefreshAll", true).SetCacheExpiration(TimeSpan.FromSeconds(5));
                                })
                                .UseFeatureFlags(flagOptions =>
                                {
                                    flagOptions.CacheExpirationInterval = TimeSpan.FromSeconds(5);
                                });
                        }); ;
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}
