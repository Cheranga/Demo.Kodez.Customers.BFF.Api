using Demo.Kodez.Customers.BFF.Api.Features.UpdateCustomer.Models;
using Demo.Kodez.Customers.BFF.Api.Shared;
using FluentValidation;
using Microsoft.FeatureManagement;

namespace Demo.Kodez.Customers.BFF.Api.Features.UpdateCustomer.Validators
{
    public class UpdateCustomerRequestValidator : ModelValidatorBase<UpdateCustomerRequest>
    {
        public UpdateCustomerRequestValidator(IFeatureManager featureManager)
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.LastName).NotNull().NotEmpty();

            var isEmailEnabled = featureManager.IsEnabledAsync(Shared.Constants.Features.UpdateEmail).Result;
            if (isEmailEnabled)
            {
                RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            }
        }
    }
}