using System.Net;
using Demo.Kodez.Customers.BFF.Api.Features.UpdateCustomer.Models;
using Demo.Kodez.Customers.BFF.Api.Shared;
using Demo.Kodez.Customers.BFF.Api.Shared.Constants;
using Demo.Kodez.Customers.BFF.Api.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Kodez.Customers.BFF.Api.Features.UpdateCustomer.Services
{
    public class UpdateCustomerResponseBuilder : IResponseBuilder<UpdateCustomerRequest, Result>
    {
        public IActionResult GetResponse(UpdateCustomerRequest request, Result response)
        {
            if (response.Status)
            {
                return new OkResult();
            }

            return response.ErrorCode switch
            {
                ErrorCodes.InvalidRequest => new BadRequestObjectResult(response.ValidationResult),
                _ => new ObjectResult(response.ValidationResult) {StatusCode = (int) (HttpStatusCode.InternalServerError)}
            };
        }
    }
}