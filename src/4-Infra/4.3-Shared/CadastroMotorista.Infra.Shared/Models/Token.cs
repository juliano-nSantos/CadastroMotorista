using System;

namespace CadastroMotorista.Infra.Shared.Models
{
    public class Token
    {
        public DateTime Created { get; set; }
        public DateTime Expiration { get; set; }
        public string AccessToken { get; set; }
    }
}