using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces;
using Repository.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public CustomerRepository(CustomerDbContext context)
        {
            _context = context;
        }

        public async Task AddCustomerAsync(Customer cliente) =>
            await _context.Customer.AddAsync(cliente);

        public void UpdateCustomer(Customer cliente) =>
            _context.Customer.Update(cliente);

        public void DeleteCustomer(Customer cliente) =>
            _context.Customer.Remove(cliente);

        public Customer GetCustomerByUserId(int userId) =>
            _context.Customer
                .Include(x => x.Users)
                .Include(x => x.Places)
                .FirstOrDefault(x => x.Users.Any(u => u.Id == userId));

        public Customer GetCustomerById(int customerId) =>
            _context.Customer
                .Include(x => x.Users)
                .Include(x => x.Places)
                .FirstOrDefault(x => x.Id == customerId);

        public bool IsEmailValid(string email) =>
            _context.Customer.Any(x => x.Email == email);

        public bool IsEmailValidForUpdate(int customerId, string email) =>
            _context.Customer.Any(x => x.Email == email && x.Id != customerId);
    }
}
