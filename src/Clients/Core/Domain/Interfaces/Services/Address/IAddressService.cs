using Domain.DTOs;

namespace Domain.Interfaces
{
    public interface IAddressService
    {
        Task<Tuple<string>> AddAddressAsync(int userId, int customerId, AddressDto address);
        Task UpdateAddressAsync(int userId, int customerId, int addressId, AddressDto address);
        Task DeleteAddressAsync(int userId, int addressId);
        AddressResponseDto GetAddressById(int userId, int addressId);
        Task<List<AddressResponseDto>> GetAddressesByCustomerIdAsync(int userId, int customerId);
    }
}
