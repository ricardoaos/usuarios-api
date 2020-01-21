using System;
namespace Users.Domain.Users.Dtos
{
    public class RecoverUserPasswordDto
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
