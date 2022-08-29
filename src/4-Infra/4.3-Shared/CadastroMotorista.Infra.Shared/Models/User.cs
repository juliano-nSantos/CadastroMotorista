using System;

namespace CadastroMotorista.Infra.Shared.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Found { get; set; }
    }
}