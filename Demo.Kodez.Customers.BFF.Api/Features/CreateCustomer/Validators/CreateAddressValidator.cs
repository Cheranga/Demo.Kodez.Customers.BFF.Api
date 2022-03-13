using Demo.Kodez.Customers.BFF.Api.Features.CreateCustomer.Models;
using Demo.Kodez.Customers.BFF.Api.Shared;
using FluentValidation;

namespace Demo.Kodez.Customers.BFF.Api.Features.CreateCustomer.Validators
{
    public class CreateAddressValidator : ModelValidatorBase<Address>
    {
        public CreateAddressValidator()
        {
            RuleFor(x => x.State).IsInEnum().NotEqual(State.Unknown);
            RuleFor(x=>x.Street).NotNull().NotEmpty().WithMessage("street cannot be empty");
            RuleFor(x=>x.Suburb).NotNull().NotEmpty().WithMessage("suburb cannot be empty");
            RuleFor(x => x.StreetNumber).NotNull().NotEmpty().WithMessage("street number cannot be empty");
            RuleFor(x => x.PostCode).GreaterThan(0);
        }
    }
}