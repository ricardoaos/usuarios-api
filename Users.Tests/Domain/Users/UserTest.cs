using Users.Domain.Users.Entities;
using Xunit;

namespace Users.Tests.Domain.Users
{
    public class UserTest
    {
        private readonly string _nome;
        private readonly string _nomeInvalido;
        private readonly string _email;
        private readonly string _emailInvalido;
        private readonly string _senha;
        private readonly string _senhaInvalido;

        public UserTest()
        {
            _nome = Faker.Name.FullName();
            _nomeInvalido = null;
            _email = Faker.Internet.Email();
            _emailInvalido = null;
            _senha= Faker.Lorem.GetFirstWord();
            _senhaInvalido = null;
        }

        [Fact]
        public void ShouldCreteUser()
        {
            Usuario usuario = new Usuario(_email, _senha, _nome);

            Assert.True(usuario.Validate());
        }

        [Fact]
        public void ShouldNotCreateUserWithoutEmail()
        {
            Usuario usuario = new Usuario(_emailInvalido, _senha, _nome);

            Assert.False(usuario.Validate());
        }

        [Fact]
        public void ShouldNotCreateUserWithoutPassword()
        {
            Usuario usuario = new Usuario(_email, _senhaInvalido, _nome);

            Assert.False(usuario.Validate());
        }

        [Fact]
        public void ShouldNotCreateUserWithoutName()
        {
            Usuario usuario = new Usuario(_email, _senha, _nomeInvalido);

            Assert.False(usuario.Validate());
        }

        [Fact]
        public void ShouldCreateUserWithSpecificEmail()
        {
            Usuario usuario = new Usuario(_email, _senha, _nome);

            Assert.Equal(usuario.Email, _email);
        }

        [Fact]
        public void ShouldCreateUserWithSpecificPassword()
        {
            Usuario usuario = new Usuario(_email, _senha, _nome);

            Assert.Equal(usuario.Senha, _senha);
        }

        [Fact]
        public void ShouldCreateUserWithSpecificName()
        {
            Usuario usuario = new Usuario(_email, _senha, _nome);

            Assert.Equal(usuario.Nome, _nome);
        }

        [Fact]
        public void ShouldUpdateEmail()
        {
            Usuario usuario = new Usuario(_email, _senha, _nome);

            Assert.Equal(usuario.Email, _email);

            string newEmail = "email@tests.com";

            usuario.UpdateEmail(newEmail);

            Assert.Equal(usuario.Email, newEmail);
        }

        [Fact]
        public void ShouldUpdatePassword()
        {
            Usuario usuario = new Usuario(_email, _senha, _nome);

            Assert.Equal(usuario.Senha, _senha);

            string newPassword = "1920384756";

            usuario.UpdatePassword(newPassword);

            Assert.Equal(usuario.Senha, newPassword);
        }

        [Fact]
        public void ShouldUpdateName()
        {
            Usuario usuario = new Usuario(_email, _senha, _nome);

            Assert.Equal(usuario.Nome, _nome);

            string newName = "Testting Test";

            usuario.UpdateName(newName);

            Assert.Equal(usuario.Nome, newName);
        }
    }
}
