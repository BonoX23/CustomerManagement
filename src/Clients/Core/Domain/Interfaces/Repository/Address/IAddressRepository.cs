using Domain.Entities;

namespace Domain.Interfaces.Repository
{
    public interface IAddressRepository : IRepository
    {
        Task AddAddressAsync(Address address);
        Task UpdateAddress(Address address);
        Task DeleteAddress(int addressId);
        Task<Address> GetAddressById(int addressId);
        Task<List<Address>> GetAddressesByCustomerId(int customerId);
    }
}
