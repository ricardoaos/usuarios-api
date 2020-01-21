using System.Threading.Tasks;
using Users.Domain.Users.Dtos;
using Users.Domain.Users.Entities;
using Users.Generics.Interfaces;

namespace Users.Domain.Users.Interfaces
{
    public interface IUserRepository : IBaseRepository<long, Usuario>
    {
        Task<Usuario> GetByIdAsync(long id);
        Task<bool> Exist(string username);
        Task<string> Login(UsuarioDto usuarioDto);
        Task<Usuario> GetByEmailAsync(string email);
    }
}
