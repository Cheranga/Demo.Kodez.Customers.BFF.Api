using System.Threading.Tasks;
using Demo.Kodez.Customers.BFF.Api.Features.CreateCustomer.Models;
using Demo.Kodez.Customers.BFF.Api.Features.CreateCustomer.Services;
using Demo.Kodez.Customers.BFF.Api.Shared;
using Demo.Kodez.Customers.BFF.Api.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Kodez.Customers.BFF.Api.Features.CreateCustomer
{
    [Route("/api/customers")]
    public class CreateCustomersController : ControllerBase
    {
        private readonly ICreateCustomerService _customerService;
        private readonly IResponseBuilder<CreateCustomerRequest, Result> _responseBuilder;

        public CreateCustomersController(ICreateCustomerService customerService,
            IResponseBuilder<CreateCustomerRequest, Result> responseBuilder)
        {
            _customerService = customerService;
            _responseBuilder = responseBuilder;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateCustomerRequest request)
        {
            var operation = await _customerService.CreateAsync(request);
            var response = _responseBuilder.GetResponse(request, operation);

            return response;
        }
    }
}