﻿
namespace Demo.Kodez.Customers.BFF.Api.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        // public static void RegisterAzureAppConfigurationProviders(this IConfigurationBuilder webHostBuilder, HostBuilderContext context, IConfigurationRoot configuration)
        // {
        //     var credentials = new DefaultAzureCredential();
        //
        //     webHostBuilder.AddAzureAppConfiguration(options =>
        //     {
        //         var sharedAzureAppConfigurationUrl = configuration["AzureAppConfigurationUrl"];
        //
        //         options.Connect(new Uri(sharedAzureAppConfigurationUrl), credentials)
        //             .Select(KeyFilter.Any)
        //             .Select("CustomersBFF:*", context.HostingEnvironment.EnvironmentName)
        //             .ConfigureKeyVault(vaultOptions => { vaultOptions.SetCredential(credentials); })
        //             .ConfigureRefresh(refreshOptions => { refreshOptions.Register("RefreshAll", true).SetCacheExpiration(TimeSpan.FromSeconds(5)); })
        //             .UseFeatureFlags(flagOptions => { flagOptions.CacheExpirationInterval = TimeSpan.FromSeconds(5); });
        //     });
        // }
    }
}