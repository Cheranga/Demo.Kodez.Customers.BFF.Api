using System.Text.Json.Serialization;

namespace Demo.Kodez.Customers.BFF.Api.Features.CreateCustomer.Models
{
    public class CreateCustomerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public Address Address { get; set; }
    }
    
    public class Address
    {
        public string StreetNumber { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public int PostCode { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public State State { get; set; }
    }
}