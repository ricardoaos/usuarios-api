using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Users.Domain.Users.Dtos;
using Users.Domain.Users.Entities;
using Users.Domain.Users.Interfaces;
using Users.Generics.Repository;
using Users.Infra.Data.Context;

namespace Users.Infra.Data.Repository
{
    public class UserRepository : BaseRepository<long, Usuario>, IUserRepository
    {
        private readonly UsersDbContext _context;
        public UserRepository(UsersDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Usuario> GetByIdAsync(long id)
        {
            return await _context.Set<Usuario>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Exist(string email)
        {
            return await _context.Set<Usuario>().Select(x => x.Email).AnyAsync(x => x == email);
        }

        public async Task<string> Login(UsuarioDto usuarioDto)
        {
            return await _context.Set<Usuario>().Where(x => x.Email == usuarioDto.Email && x.Senha == usuarioDto.Senha).Select(x => x.Email).FirstOrDefaultAsync();
        }

        public async Task<Usuario> GetByEmailAsync(string email)
        {
            return await _context.Set<Usuario>().FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
