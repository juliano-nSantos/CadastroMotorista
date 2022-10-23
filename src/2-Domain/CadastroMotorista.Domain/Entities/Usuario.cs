using System;

namespace CadastroMotorista.Domain.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Permissao { get; set; }
        public bool Exists { get; set; }
    }
}
