using System;

namespace CadastroMotorista.Domain.Models
{
    [Serializable]
    public class FiltroBusca
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Sexo { get; set; }
        public int IdadeInicial { get; set; }
        public int IdadeFinal { get; set; }
        public bool? Ativo { get; set; }
    }
}
