using CadastroMotorista.Infra.Shared.Models.Base;

namespace CadastroMotorista.Infra.Shared.Models
{
    public class LogRequest : LogBase
    {
        public string Client { get; set; }
        public object Request { get; set; }
    }
}