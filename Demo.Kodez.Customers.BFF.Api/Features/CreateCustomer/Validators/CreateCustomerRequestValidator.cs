using Demo.Kodez.Customers.BFF.Api.Features.CreateCustomer.Models;
using Demo.Kodez.Customers.BFF.Api.Shared;
using FluentValidation;

namespace Demo.Kodez.Customers.BFF.Api.Features.CreateCustomer.Validators
{
    public class CreateCustomerRequestValidator : ModelValidatorBase<CreateCustomerRequest>
    {
        public CreateCustomerRequestValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x=>x.LastName).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Address).SetValidator(new CreateAddressValidator());
        }
    }
}