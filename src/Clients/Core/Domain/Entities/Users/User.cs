using Domain.Interfaces;
using Newtonsoft.Json;

namespace Domain.Entities
{
    public class User : IEntityBase
    {
        public int Id { get; set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public bool Actived { get; private set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public int CustomerId { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }

        public User()
        {
        }

        /// <summary>
        /// Criar Usuario
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public User(string login, string password)
        {
            Actived = true;
            Login = login;
            Password = password;
        }

        /// <summary>
        /// Atualizar senha
        /// </summary>
        /// <param name="password"></param>
        public void UpdatePassword(string password)
        {
            this.Password = password;
        }

        /// <summary>
        /// Atualizar status
        /// </summary>
        /// <param name="actived"></param>
        public void UpdateStatus(bool actived)
        {
            Actived = actived;
        }

        /// <summary>
        /// Associar Cliente
        /// </summary>
        /// <param name="customerId"></param>
        public void AssociateClient(int customerId)
        {
            CustomerId = customerId;
        }
    }
}
