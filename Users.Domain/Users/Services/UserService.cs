using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Users.Dtos;
using Users.Domain.Users.Entities;
using Users.Domain.Users.Interfaces;
using Users.Generics.Helpers;
using Users.Generics.Resources;

namespace Users.Domain.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;

        public UserService(IUserRepository userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        public async Task<UsuarioDto> Get(long id)
        {
            return UsuarioDto.CreateUsuarioDto(await _userRepository.GetByIdAsync(id));
        }

        public async Task<List<UsuarioDto>> Get()
        {
            return _userRepository.ToListAsync().Result.Select(UsuarioDto.CreateUsuarioDto).ToList();
        }

        public async Task<UsuarioDto> Save(UsuarioDto usuarioDto)
        {
            if (usuarioDto.Id > 0)
                return await Update(usuarioDto);

            return await Create(usuarioDto);
        }

        public async Task Delete(long id)
        {
            Usuario user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new ArgumentNullException(StringResource.ValidationMessageUserDontExists);
            
            _userRepository.Remove(user);
        }

        public async Task<UsuarioDto> Login(UsuarioDto usuarioDto)
        {
            string username = await _userRepository.Login(usuarioDto);

            if (username == null)
                throw new ArgumentNullException(StringResource.ValidationMessageUserDontExists);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            usuarioDto.Token = tokenHandler.WriteToken(token);

            return usuarioDto;
        }

        public async Task<string> Recover(RecoverUserPasswordDto recoverDto)
        {
            if (!await _userRepository.Exist(recoverDto.Email))
                throw new Exception(StringResource.ValidationMessageUserDontExists);

            Usuario usuario = await _userRepository.GetByEmailAsync(recoverDto.Email);

            usuario.UpdatePassword(recoverDto.Senha);

            if (!usuario.Validate())
                throw new ArgumentException(StringResource.ValidationMessageInvalidUser);

            _userRepository.Update(usuario);

            return StringResource.RecoverMessageSuccess;
        }

        private async Task<UsuarioDto> Update(UsuarioDto usuarioDto)
        {
            Usuario usuario = await _userRepository.GetByIdAsync(usuarioDto.Id);

            if (usuario == null)
                throw new Exception(StringResource.ValidationMessageUserAlreadyExist);

            usuario.UpdateEmail(usuarioDto.Email);
            usuario.UpdatePassword(usuarioDto.Senha);
            usuario.UpdateName(usuarioDto.Nome);

            if (!usuario.Validate())
                throw new ArgumentException(StringResource.ValidationMessageInvalidUser);

            _userRepository.Update(usuario);

            return usuarioDto;
        }

        private async Task<UsuarioDto> Create(UsuarioDto usuarioDto)
        {
            if (await _userRepository.Exist(usuarioDto.Email))          
                throw new Exception(StringResource.ValidationMessageUserAlreadyExist);

            Usuario usuario = new Usuario(usuarioDto.Email, usuarioDto.Senha, usuarioDto.Nome);

            if (!usuario.Validate())
                throw new ArgumentException(StringResource.ValidationMessageInvalidUser);

            await _userRepository.AddAsync(usuario);

            return UsuarioDto.CreateUsuarioDto(usuario);
        }
    }
}
