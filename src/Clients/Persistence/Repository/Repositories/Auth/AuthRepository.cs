using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces;
using Repository.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly CustomerDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public AuthRepository(CustomerDbContext context)
        {
            _context = context;
        }

        public async Task<User> AutenticateAsync(string login, string password) =>
            await _context.User
                .Include(x=>x.Customer)
                .FirstOrDefaultAsync(x => x.Login.ToLower() == login.ToLower() && x.Password == password);

        public User GetUserById(int userId) =>
            _context.User.Find(userId);

        public void UpdateUserPassword(User user) =>
            _context.User.Update(user);
    }
}
