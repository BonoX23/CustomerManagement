using Domain.DTOs;

namespace Domain.Interfaces
{
    public interface ICustomerService
    {
        Task<Tuple<string>> AddCustomerAsync(CustomerDto customerDto);
        Task UpdateCustomerAsync(int userId, int customerId, CustomerDto customerDto);
        Task DeleteCustomerAsync(int userId, int customerId);
        CustomerResponseDto GetCustomerById(int userId, int customerId);
    }
}
