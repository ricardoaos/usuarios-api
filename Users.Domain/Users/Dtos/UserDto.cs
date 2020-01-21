using Users.Domain.Users.Entities;

namespace Users.Domain.Users.Dtos
{
    public class UsuarioDto
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Token { get; set; }

        public static UsuarioDto CreateUsuarioDto(Usuario usuario)
        {
            return new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Senha = usuario.Senha,
                Email = usuario.Email
            };
        }
    }
}
