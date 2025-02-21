using Domain.Interfaces;
using Domain.Utils;
using FluentValidation;
using Newtonsoft.Json;

namespace Domain.Entities
{
    public class Address : Validation, IEntityBase
    {
        public int Id { get; set; }
        public string Place { get; private set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public int CustomerId { get; private set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }

        public Address()
        {
        }

        /// <summary>
        /// Criar Logradouro
        /// </summary>
        /// <param name="place"></param>
        /// <param name="customerId"></param>
        public Address(string place, int customerId)
        {
            this.CustomerId = customerId;
            this.Place = place;
        }

        /// <summary>
        /// Atualizar Logradouro
        /// </summary>
        /// <param name="place"></param>
        public void Update(string place)
        {
            Place = place;
        }

        /// <summary>
        /// Metodo para validação dos campos
        /// </summary>
        public void Validate()
        {
            Validate(this, new AddressValidation());
        }
    }

    /// <summary>
    /// Validação dos dados do logradouro
    /// </summary>
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(x => x.Place)
                .NotEmpty()
                .WithMessage("Logradouro é obrigatório")
                .MaximumLength(100)
                .WithMessage("Logradouro deve conter no maximo 100 caracters");
        }
    }
}
