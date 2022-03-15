using Newtonsoft.Json;

namespace Demo.Kodez.Customers.BFF.Api.Features.UpdateCustomer.Models
{
    public class UpdateCustomerRequest
    {
        [JsonIgnore]
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
    }
}