using System.Threading.Tasks;
using Demo.Kodez.Customers.BFF.Api.Features.UpdateCustomer.Models;
using Demo.Kodez.Customers.BFF.Api.Shared;
using Demo.Kodez.Customers.BFF.Api.Shared.Constants;
using Demo.Kodez.Customers.BFF.Api.Shared.Services;
using FluentValidation;
using Microsoft.FeatureManagement;

namespace Demo.Kodez.Customers.BFF.Api.Features.UpdateCustomer.Services
{
    public interface IUpdateCustomerService
    {
        Task<Result> UpdateAsync(UpdateCustomerRequest request);
    }
    
    public class UpdateCustomerService : IUpdateCustomerService
    {
        private readonly IValidator<UpdateCustomerRequest> _validator;
        private readonly ICustomerIdentityService _customerIdentityService;

        public UpdateCustomerService(IValidator<UpdateCustomerRequest> validator, ICustomerIdentityService customerIdentityService)
        {
            _validator = validator;
            _customerIdentityService = customerIdentityService;
        }
        
        public async Task<Result> UpdateAsync(UpdateCustomerRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return Result.Failure(ErrorCodes.InvalidRequest, validationResult);
            }

            var updateRequest = new UpsertCustomerIdentityRequest
            {
                CustomerId = request.CustomerId,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            var operation = await _customerIdentityService.UpdateAsync(updateRequest);

            return operation;
        }
    }
}