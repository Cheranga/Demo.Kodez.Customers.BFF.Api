using System;
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
        private readonly ICustomerIdentityService _customerIdentityService;
        private readonly ILogger<CreateCustomerService> _logger;
        private readonly IValidator<CreateCustomerRequest> _validator;

        public CreateCustomerService(IValidator<CreateCustomerRequest> validator, ICustomerIdentityService customerIdentityService, ILogger<CreateCustomerService> logger)
        {
            _validator = validator;
            _customerIdentityService = customerIdentityService;
            _logger = logger;
        }

        public async Task<Result> CreateAsync(CreateCustomerRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                _logger.LogError(ErrorMessages.InvalidRequest);
                return Result.Failure(ErrorCodes.InvalidRequest, validationResult);
            }

            var identityOperation = await SaveCustomerIdentityDataAsync(request);

            return identityOperation;
        }

        private async Task<Result> SaveCustomerIdentityDataAsync(CreateCustomerRequest request)
        {
            var upsertRequest = new UpsertCustomerIdentityRequest
            {
                CustomerId = Guid.NewGuid().ToString("N").ToUpper(),
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            return await _customerIdentityService.InsertAsync(upsertRequest);
        }
    }
}