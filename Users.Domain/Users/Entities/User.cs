using FluentValidation;
using Users.Generics.Domain;

namespace Users.Domain.Users.Entities
{
    public class Usuario : Entity<long, Usuario>
    {
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public string Nome { get; private set; }

        protected Usuario() { }

        public Usuario(string email,
                    string senha,
                    string nome)
        {
            Email = email;
            Senha = senha;
            Nome = nome;
        }

        public override bool Validate()
        {
            RuleFor(_ => _.Email)
                .NotNull()
                .NotEmpty();

            RuleFor(_ => _.Senha)
                .NotNull()
                .NotEmpty();

            RuleFor(_ => _.Nome)
                .NotNull()
                .NotEmpty();

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        public void UpdateEmail(string email)
        {
            Email = email;
        }

        public void UpdatePassword(string senha)
        {
            Senha = senha;
        }

        public void UpdateName(string nome)
        {
            Nome = nome;
        }
    }
}
