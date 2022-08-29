using System.ComponentModel.DataAnnotations;

namespace CadastroMotorista.Domain.Dtos
{
    public class ExampleDto
    {
        public int IdExample { get; set; }

        [Required(ErrorMessage = "Nome é de preenchimento obrigatório")]
        [StringLength(100, ErrorMessage = "Nome deve conter entre 5 e 100 caracteres", MinimumLength = 5)]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-mail é de preenchimento obrigatório")]
        [EmailAddress(ErrorMessage = "Insira um e-mail válido")]
        public string Email { get; set; }
    }
}