using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Demo.Kodez.Customers.BFF.Api.Shared.Constants;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Demo.Kodez.Customers.BFF.Api.Shared.Services
{
    public interface ICustomerProfileService
    {
        Task<Result> SaveAsync(UpsertCustomerProfileRequest request);
    }

    public class CustomerProfileService : ICustomerProfileService
    {
        private readonly CustomerProfileServiceConfig _config;
        private readonly HttpClient _httpClient;
        private readonly ILogger<CustomerProfileService> _logger;

        public CustomerProfileService(HttpClient httpClient, CustomerProfileServiceConfig config, ILogger<CustomerProfileService> logger)
        {
            _httpClient = httpClient;
            _config = config;
            _logger = logger;
        }

        public async Task<Result> SaveAsync(UpsertCustomerProfileRequest request)
        {
            try
            {
                var url = $"{_config.BaseUrl}/api/customers";
                var serializedData = JsonConvert.SerializeObject(request);

                var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(serializedData, Encoding.UTF8, "application/json")
                };

                var httpResponse = await _httpClient.SendAsync(httpRequest);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return Result.Failure(ErrorCodes.CannotUpsertCustomer, httpResponse.ReasonPhrase);
                }

                return Result.Success();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, ErrorMessages.CustomerProfileApiError);
            }

            return Result.Failure(ErrorCodes.CustomerProfileApiError, ErrorMessages.CustomerProfileApiError);
        }
    }
}