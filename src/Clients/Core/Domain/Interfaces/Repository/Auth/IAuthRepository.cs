using Domain.Entities;

namespace Domain.Interfaces.Repository
{
    public interface IAuthRepository : IRepository
    {
        User GetUserById(int userId);
        Task<User> AutenticateAsync(string login, string password);
        void UpdateUserPassword(User user);
    }
}
