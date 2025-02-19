using Domain.Interfaces;
using Domain.Utils;
using FluentValidation;

namespace Domain.Entities
{
    public class Customer : Validation, IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Logo { get; private set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Address> Places { get; set; }

        public Customer()
        {
            Users = new List<User>();
            Places = new HashSet<Address>();
        }

        /// <summary>
        /// Criar Cliente
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="logo"></param>
        public Customer(string name, string email, string logo)
        {
            Name = name;
            Email = email;
            Logo = logo;

            Users = new List<User>();
            Places = new HashSet<Address>();
        }

        /// <summary>
        /// Atualizar Cliente
        /// </summary>
        /// <param name="name"></param>
        /// <param name="logo"></param>
        public void Update(string name, string logo)
        {
            Name = name;
            Logo = logo;
        }

        /// <summary>
        /// Metodo para validação dos campos
        /// </summary>
        public void Validate()
        {
            Validate(this, new CustomerValidation());
        }
    }

    /// <summary>
    /// Validação dos dados do cliente
    /// </summary>
    public class CustomerValidation : AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Nome é obrigatório")
                .MaximumLength(100)
                .WithMessage("Nome deve conter no maximo 100 caracters");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email é obrigatório")
                .MaximumLength(100)
                .WithMessage("Nome deve conter no maximo 100 caracters");

            RuleFor(x => x.Logo)
                .NotEmpty()
                .WithMessage("Logotipo é obrigatório")
                .MaximumLength(150)
                .WithMessage("Nome deve conter no maximo 100 caracters");

            RuleFor(x => x.Places)
                .NotEmpty()
                .WithMessage("É obrigatório pelo menos um logradouro");

            RuleForEach(x => x.Places).ChildRules(order =>
            {
                order.RuleFor(l => l.Place)
                .NotEmpty()
                .WithMessage("Logradouro é obrigatório")
                .MaximumLength(100)
                .WithMessage("Logradouro deve conter no maximo 100 caracters");
            });
        }
    }
}
