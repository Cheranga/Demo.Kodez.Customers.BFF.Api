using System;
using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Demo.Kodez.Customers.BFF.Api.Extensions
{
    public static class WebHostBuilderExtensions
    {
        public static void RegisterAzureAppConfigurationProviders(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.ConfigureAppConfiguration(builder =>
            {
                var config = builder.Build();
                var sharedAzureAppConfigurationUrl = config["AzureAppConfigurationUrl"];
                var customersBffAzureAppConfigurationUrl = config["CustomersBFFAzureAppConfigurationUrl"];

                var credentials = new DefaultAzureCredential();

                builder.AddAzureAppConfiguration(options =>
                {
                    options.Connect(new Uri(sharedAzureAppConfigurationUrl), credentials)
                        .ConfigureKeyVault(vaultOptions => { vaultOptions.SetCredential(credentials); })
                        .ConfigureRefresh(refreshOptions => { refreshOptions.Register("RefreshAll", true).SetCacheExpiration(TimeSpan.FromSeconds(5)); })
                        .UseFeatureFlags(flagOptions => { flagOptions.CacheExpirationInterval = TimeSpan.FromSeconds(5); });
                }).AddAzureAppConfiguration(options =>
                {
                    options.Connect(new Uri(customersBffAzureAppConfigurationUrl), credentials)
                        .ConfigureKeyVault(vaultOptions => { vaultOptions.SetCredential(credentials); })
                        .ConfigureRefresh(refreshOptions => { refreshOptions.Register("RefreshAll", true).SetCacheExpiration(TimeSpan.FromSeconds(5)); })
                        .UseFeatureFlags(flagOptions => { flagOptions.CacheExpirationInterval = TimeSpan.FromSeconds(5); });
                });
            });
        }
    }
}