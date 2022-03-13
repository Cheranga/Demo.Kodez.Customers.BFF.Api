namespace Demo.Kodez.Customers.BFF.Api.Shared.Constants
{
    public static class ErrorCodes
    {
        public const string InvalidRequest = nameof(InvalidRequest);
        public const string CannotUpsertCustomer = nameof(CannotUpsertCustomer);
        public const string CustomerIdentityApiError = nameof(CustomerIdentityApiError);
        public const string CustomerProfileApiError = nameof(CustomerProfileApiError);
    }

    public static class ErrorMessages
    {
        public const string InvalidRequest = "invalid request";
        public const string CannotUpsertCustomer = "error occurred when insert/update customer";
        public const string CustomerIdentityApiError = "error occurred when calling customer identity API";
        public const string CustomerProfileApiError = "error occurred when calling customer profile API";
    }
}