using System.Linq;
using System.Net;
using Demo.Kodez.Customers.BFF.Api.Features.CreateCustomer.Models;
using Demo.Kodez.Customers.BFF.Api.Shared;
using Demo.Kodez.Customers.BFF.Api.Shared.Constants;
using Demo.Kodez.Customers.BFF.Api.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Kodez.Customers.BFF.Api.Features.CreateCustomer.Services
{
    public class CreateCustomerResponseBuilder : IResponseBuilder<CreateCustomerRequest, Result>
    {
        public IActionResult GetResponse(CreateCustomerRequest request, Result operation)
        {
            if (operation.Status)
            {
                return new OkResult();
            }

            return GetErrorResponse(operation);
        }

        private IActionResult GetErrorResponse(Result operation)
        {
            var errorResponse = new
            {
                operation.ErrorCode,
                Errors = operation.ValidationResult.Errors.Select(x =>
                    new
                    {
                        x.PropertyName,
                        x.ErrorMessage
                    })
            };
            return operation.ErrorCode switch
            {
                ErrorCodes.InvalidRequest => new BadRequestObjectResult(errorResponse),
                _ => new ObjectResult(errorResponse)
                {
                    StatusCode = (int) HttpStatusCode.InternalServerError
                }
            };
        }
    }
}