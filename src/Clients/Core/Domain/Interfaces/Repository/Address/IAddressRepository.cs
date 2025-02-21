using Domain.Entities;

namespace Domain.Interfaces.Repository
{
    public interface IAddressRepository : IRepository
    {
        Task AddAddressAsync(Address address);
        void UpdateAddress(Address address);
        void DeleteAddress(Address address);
        Address GetAddressById(int addressId);
        Task<List<Address>> GetAddressesByCustomerId(int customerId);
    }
}
