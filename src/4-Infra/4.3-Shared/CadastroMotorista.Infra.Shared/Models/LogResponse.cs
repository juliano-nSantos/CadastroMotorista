using CadastroMotorista.Infra.Shared.Models.Base;

namespace CadastroMotorista.Infra.Shared.Models
{
    public class LogResponse : LogBase
    {
        public object Response { get; set; }
    }
}