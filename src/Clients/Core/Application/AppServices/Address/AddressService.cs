using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Notifications;

namespace Application.AppServices
{
    public class AddressService: IAddressService
    {
        private readonly INotificationContext _notification;
        private readonly IAddressRepository _repository;
        private readonly ICustomerRepository _customerRepository;

        public AddressService(INotificationContext notification,
                                 IAddressRepository repository,
                                 ICustomerRepository customerRepository)
        {
            _repository = repository;
            _notification = notification;
            _customerRepository = customerRepository;
        }

        public async Task<Tuple<string>> AddAddressAsync(int userId, int customerId, AddressDto address)
        {
            var customerDomain = _customerRepository.GetCustomerById(customerId);

            if (customerDomain == null)
                _notification.AddNotification(new Notification("Cliente informado não existe"));

            if (customerDomain != null && !customerDomain.Users.All(x => x.Id == userId))
            {
                _notification.AddNotification(new Notification("Ação não permitida, permissão negada!"));
            }

            if (!_notification.HasNotifications)
            {
                var addressDomain = new Address(address.Place, customerId);

                addressDomain.Validate();

                if (addressDomain.Invalid)
                    _notification.AddNotificationFromValidationResult(addressDomain.ValidationResult);
                else
                {
                    await _repository.AddAddressAsync(addressDomain);
                    await _repository.UnitOfWork.Commit();
                }
            }

            return new Tuple<string>($"Logradouro adicionado ao cliente {customerDomain.Name} com sucesso");
        }

        public async Task DeleteAddressAsync(int userId, int addressId)
        {
            var addressDomain = _repository.GetAddressById(addressId);

            if (addressDomain == null)
                _notification.AddNotification(new Notification("Logradouro não existe"));

            if (addressDomain != null && !addressDomain.Customer.Users.All(x => x.Id == userId))
            {
                _notification.AddNotification(new Notification("Ação não permitida, permissão negada!"));
            }

            if (_notification.HasNotifications)
                return;

            _repository.DeleteAddress(addressDomain);
            await _repository.UnitOfWork.Commit();
        }

        public AddressResponseDto GetAddressById(int userId, int addressId)
        {
            var addressDomain = _repository.GetAddressById(addressId);

            if (addressDomain == null)
                _notification.AddNotification(new Notification("Logradouro não existe"));

            if (addressDomain != null && !addressDomain.Customer.Users.All(x => x.Id == userId))
            {
                _notification.AddNotification(new Notification("Ação não permitida, permissão negada!"));
            }

            if (_notification.HasNotifications)
                return null;
            else
                return new AddressResponseDto
                {
                    Id = addressDomain.Id,
                    CustomerId = addressDomain.CustomerId,
                    Place = addressDomain.Place,
                };
        }

        public async Task<List<AddressResponseDto>> GetAddressesByCustomerIdAsync(int userId, int customerId)
        {
            var customerDomain = _customerRepository.GetCustomerById(customerId);

            if (customerDomain == null)
                _notification.AddNotification(new Notification("Cliente informado não existe"));

            if (customerDomain != null && !customerDomain.Users.All(x => x.Id == userId))
            {
                _notification.AddNotification(new Notification("Ação não permitida, permissão negada!"));
            }

            var listAddress = await _repository.GetAddressesByCustomerId(customerId);

            if (_notification.HasNotifications)
                return null;
            else
                return listAddress.Select(x=> new AddressResponseDto
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    Place = x.Place,
                }).ToList();
        }

        public async Task UpdateAddressAsync(int userId, int customerId, int addressId, AddressDto address)
        {
            var addressDomain = _repository.GetAddressById(addressId);

            if (addressDomain == null)
                _notification.AddNotification(new Notification("Logradouro não existe"));

            if (addressDomain != null && !addressDomain.Customer.Users.All(x => x.Id == userId) && addressDomain.CustomerId != customerId)
            {
                _notification.AddNotification(new Notification("Ação não permitida, permissão negada!"));
            }

            if (!_notification.HasNotifications)
            {
                addressDomain.Update(address.Place);

                addressDomain.Validate();

                if (addressDomain.Invalid)
                    _notification.AddNotificationFromValidationResult(addressDomain.ValidationResult);
                else
                {
                    _repository.UpdateAddress(addressDomain);
                    await _repository.UnitOfWork.Commit();
                }
            }
        }
    }
}