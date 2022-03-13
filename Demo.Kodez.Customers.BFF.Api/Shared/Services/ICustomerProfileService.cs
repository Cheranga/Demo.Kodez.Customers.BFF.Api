using System.Threading.Tasks;

namespace Demo.Kodez.Customers.BFF.Api.Shared.Services
{
    public class UpsertCustomerProfileRequest
    {
        
    }
    
    public interface ICustomerProfileService
    {
        Task<Result> SaveAsync(UpsertCustomerProfileRequest request);
    }
    
    public class CustomerProfileService : ICustomerProfileService
    {
        public Task<Result> SaveAsync(UpsertCustomerProfileRequest request)
        {
            return Task.FromResult(Result.Success());
        }
    }
}