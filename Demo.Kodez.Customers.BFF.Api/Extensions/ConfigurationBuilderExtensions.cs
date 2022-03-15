using System;
using Azure.Identity;
using Demo.Kodez.Customers.BFF.Api.Shared.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;

namespace Demo.Kodez.Customers.BFF.Api.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static void RegisterAzureAppConfigurationProviders(this IConfigurationBuilder webHostBuilder, HostBuilderContext context, IConfigurationRoot configuration)
        {
            var credentials = new DefaultAzureCredential();

            webHostBuilder.AddAzureAppConfiguration(options =>
            {
                var sharedAzureAppConfigurationUrl = configuration[ConfigurationOptions.AzureAppConfigurationUrl];

                options.Connect(new Uri(sharedAzureAppConfigurationUrl), credentials)
                    .Select(KeyFilter.Any)
                    .Select($"{ConfigurationOptions.AppPrefix}:*", context.HostingEnvironment.EnvironmentName)
                    .ConfigureKeyVault(vaultOptions => { vaultOptions.SetCredential(credentials); })
                    .ConfigureRefresh(refreshOptions => { refreshOptions.Register(ConfigurationOptions.SentinelKey, true).SetCacheExpiration(TimeSpan.FromSeconds(5)); })
                    .UseFeatureFlags(flagOptions => { flagOptions.CacheExpirationInterval = TimeSpan.FromSeconds(5); });
            });
        }
    }
}