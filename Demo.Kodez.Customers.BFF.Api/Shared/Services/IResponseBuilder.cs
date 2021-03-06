using Microsoft.AspNetCore.Mvc;

namespace Demo.Kodez.Customers.BFF.Api.Shared.Services
{
    public interface IResponseBuilder<in TRequest, in TResponse>
    {
        IActionResult GetResponse(TRequest request, TResponse response);
    }
}