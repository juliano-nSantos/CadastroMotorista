namespace CadastroMotorista.Domain.Entities
{
    public class Motorista
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Sexo { get; set; }
        public int Idade { get; set; }
        public bool? Ativo { get; set; }
    }
}
