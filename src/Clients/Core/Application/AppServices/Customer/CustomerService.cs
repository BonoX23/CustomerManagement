using Domain.DTOs;
using Domain.Utils;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Notifications;

namespace Application.AppServices
{
    public class CustomerService : ICustomerService
    {
        private readonly INotificationContext _notification;
        private readonly ICustomerRepository _repository;

        public CustomerService(INotificationContext notification,
                                 ICustomerRepository repository)
        {
            _repository = repository;
            _notification = notification;
        }

        public async Task<Tuple<string>> AddCustomerAsync(CustomerDto customer)
        {
            if (!string.IsNullOrEmpty(customer.Email) && _repository.IsEmailValid(customer.Email))
                _notification.AddNotification(new Notification("E-mail já cadastrado"));

            var passwordGuid = Guid.NewGuid().ToString().ToUpper().Split('-')[0];
            if (!_notification.HasNotifications)
            {
                var passwordHash = customer.Email.GenerateHashPassword(passwordGuid);

                var userDomain = new User(customer.Email, passwordHash);
                var customerDomain = new Customer(customer.Name, customer.Email, customer.Logo);

                customerDomain.Users.Add(userDomain);

                foreach (var item in customer.Places)
                    customerDomain.Places.Add(new Address(item.Place, customerDomain.Id));

                customerDomain.Validate();

                if (customerDomain.Invalid)
                    _notification.AddNotificationFromValidationResult(customerDomain.ValidationResult);
                else
                {
                    await _repository.AddCustomerAsync(customerDomain);
                    await _repository.UnitOfWork.Commit();
                }
            }

            return new Tuple<string>($"Cadastro realizado com sucesso, utilize seu email como login e o codigo {passwordGuid} como senha");
        }

        public async Task DeleteCustomerAsync(int userId, int customerId)
        {
            var customerDomain = _repository.GetCustomerById(customerId);

            if (customerDomain == null)
                _notification.AddNotification(new Notification("Cliente não existe"));

            if (customerDomain != null && !customerDomain.Users.All(x => x.Id == userId))
            {
                _notification.AddNotification(new Notification("Ação não permitida, permissão negada!"));
            }

            if (_notification.HasNotifications)
                return;

            _repository.DeleteCustomer(customerDomain);
            await _repository.UnitOfWork.Commit();
        }

        public CustomerResponseDto GetCustomerById(int userId, int customerId)
        {
            var customerDomain = _repository.GetCustomerById(customerId);

            if (customerDomain == null)
                _notification.AddNotification(new Notification("Cliente não existe"));

            if (customerDomain != null && !customerDomain.Users.All(x => x.Id == userId))
            {
                _notification.AddNotification(new Notification("Ação não permitida, permissão negada!"));
            }

            if (_notification.HasNotifications)
                return null;
            else
                return new CustomerResponseDto
                {
                    Id = customerDomain.Id,
                    Email = customerDomain.Email,
                    Logo = customerDomain.Logo,
                    Name = customerDomain.Name,
                    Places = customerDomain.Places.Select(x => new AddressResponseDto
                    {
                        Id = x.Id,
                        CustomerId = x.CustomerId,
                        Place = x.Place
                    }).ToList()
                };
        }

        public async Task UpdateCustomerAsync(int userId, int customerId, CustomerDto customerDto)
        {
            Console.WriteLine($"Recebendo atualização para ID: {customerId}");
            Console.WriteLine($"Nome: {customerDto.Name}, Email: {customerDto.Email}, Logo: {customerDto.Logo}");

            var customerDomain = _repository.GetCustomerById(customerId);

            if (customerDomain == null)
            {
                _notification.AddNotification(new Notification("Cliente não existe"));
                Console.WriteLine("Erro: Cliente não encontrado!");
            }

            if (customerDomain != null && !customerDomain.Users.All(x => x.Id == userId))
            {
                _notification.AddNotification(new Notification("Ação não permitida, permissão negada!"));
                Console.WriteLine("Erro: Permissão negada!");
            }

            if (!string.IsNullOrEmpty(customerDto.Email) && _repository.IsEmailValidForUpdate(customerId, customerDto.Email))
            {
                _notification.AddNotification(new Notification("E-mail já cadastrado"));
                Console.WriteLine("Erro: E-mail já cadastrado!");
            }

            if (!_notification.HasNotifications)
            {
                customerDomain.Places.Clear();
                customerDomain.Update(customerDto.Name, customerDto.Logo);

                foreach (var item in customerDto.Places)
                    customerDomain.Places.Add(new Address(item.Place, customerDomain.Id));

                customerDomain.Validate();

                if (customerDomain.Invalid)
                {
                    _notification.AddNotificationFromValidationResult(customerDomain.ValidationResult);
                    Console.WriteLine("Erro: Validação do cliente falhou!");
                }
                else
                {
                    _repository.UpdateCustomer(customerDomain);
                    await _repository.UnitOfWork.Commit();
                    Console.WriteLine("Cliente atualizado com sucesso!");
                }
            }
        }
    }
}
