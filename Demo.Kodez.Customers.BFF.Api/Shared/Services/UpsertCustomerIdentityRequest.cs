namespace Demo.Kodez.Customers.BFF.Api.Shared.Services
{
    public class UpsertCustomerIdentityRequest
    {
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}