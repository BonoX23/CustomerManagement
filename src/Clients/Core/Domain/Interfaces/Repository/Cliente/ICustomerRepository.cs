using Domain.Entities;

namespace Domain.Interfaces.Repository
{
    public interface ICustomerRepository : IRepository
    {
        Task AddCustomerAsync(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
        Customer GetCustomerByUserId(int userId);
        Customer GetCustomerById(int customerId);
        bool IsEmailValid(string email);
        bool IsEmailValidForUpdate(int customerId, string email);
    }
}
