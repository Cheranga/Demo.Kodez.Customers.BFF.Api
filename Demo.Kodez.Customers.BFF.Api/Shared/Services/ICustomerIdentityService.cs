using System.Threading.Tasks;

namespace Demo.Kodez.Customers.BFF.Api.Shared.Services
{
    public class UpsertCustomerIdentityRequest
    {
        
    }
    
    public interface ICustomerIdentityService
    {
        Task<Result> SaveAsync(UpsertCustomerIdentityRequest request);
    }
    
    public class CustomerIdentityService : ICustomerIdentityService
    {
        public Task<Result> SaveAsync(UpsertCustomerIdentityRequest request)
        {
            return Task.FromResult(Result.Success());
        }
    }
}