using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Users.Dtos;

namespace Users.Domain.Users.Interfaces
{
    public interface IUserService
    {
        Task<List<UsuarioDto>> Get();
        Task<UsuarioDto> Get(long id);
        Task<UsuarioDto> Save(UsuarioDto userDto);
        Task Delete(long id);
        Task<UsuarioDto> Login(UsuarioDto userDto);
        Task<string> Recover(RecoverUserPasswordDto recoverDto);
    }
}
