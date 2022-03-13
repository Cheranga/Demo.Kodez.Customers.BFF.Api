using System.Threading.Tasks;
using Demo.Kodez.Customers.BFF.Api.Features.CreateCustomer.Models;
using Demo.Kodez.Customers.BFF.Api.Shared;
using Demo.Kodez.Customers.BFF.Api.Shared.Constants;
using Demo.Kodez.Customers.BFF.Api.Shared.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Demo.Kodez.Customers.BFF.Api.Features.CreateCustomer.Services
{
    public interface ICreateCustomerService
    {
        Task<Result> CreateAsync(CreateCustomerRequest request);
    }
    
    public class CreateCustomerService : ICreateCustomerService
    {
        private readonly IValidator<CreateCustomerRequest> _validator;
        private readonly ICustomerIdentityService _customerIdentityService;
        private readonly ICustomerProfileService _customerProfileService;
        private readonly ILogger<CreateCustomerService> _logger;

        public CreateCustomerService(IValidator<CreateCustomerRequest> validator, ICustomerIdentityService customerIdentityService, ICustomerProfileService customerProfileService,  ILogger<CreateCustomerService> logger)
        {
            _validator = validator;
            _customerIdentityService = customerIdentityService;
            _customerProfileService = customerProfileService;
            _logger = logger;
        }
        
        public async Task<Result> CreateAsync(CreateCustomerRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return Result.Failure(ErrorCodes.InvalidRequest, validationResult);
            }

            var identityOperation = await SaveCustomerIdentityDataAsync(request);
            var profileOperation = await SaveCustomerProfileDataAsync(request);

            if (!identityOperation.Status)
            {
                return identityOperation;
            }

            if (!profileOperation.Status)
            {
                return profileOperation;
            }
            
            return Result.Success();
        }

        private async Task<Result> SaveCustomerIdentityDataAsync(CreateCustomerRequest request)
        {
            var upsertRequest = new UpsertCustomerIdentityRequest
            {

            };

            return await _customerIdentityService.SaveAsync(upsertRequest);
        }

        private async Task<Result> SaveCustomerProfileDataAsync(CreateCustomerRequest request)
        {
            var upsertRequest = new UpsertCustomerProfileRequest
            {

            };

            return await _customerProfileService.SaveAsync(upsertRequest);
        }
    }
}