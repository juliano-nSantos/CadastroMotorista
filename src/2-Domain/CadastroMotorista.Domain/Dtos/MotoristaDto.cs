using System.ComponentModel.DataAnnotations;

namespace CadastroMotorista.Domain.Dtos
{
    public class MotoristaDto
    {
        public int Codigo { get; set; }
        [Required(ErrorMessage = "Campo Nome é de preenchimento obrigatório")]
        [StringLength(100, ErrorMessage = "Campo Nome deve conter entre 5 e 100 caracteres", MinimumLength = 5)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo CPF é de preenchimento obrigatório")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "Campo Sexo é de preenchimento obrigatório")]
        [StringLength(1, ErrorMessage = "Campo Sexo deve conter 1 caractere, F - Feminino, M - Masculino, O - Outro ")]
        public string Sexo { get; set; }
        public int Idade { get; set; }
        public bool? Ativo { get; set; }        
    }
}
