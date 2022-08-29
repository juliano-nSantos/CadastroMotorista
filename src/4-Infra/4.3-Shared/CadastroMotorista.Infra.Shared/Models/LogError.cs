using CadastroMotorista.Infra.Shared.Models.Base;

namespace CadastroMotorista.Infra.Shared.Models
{
    public class LogError : LogBase
    {
        public string ErrorMessage { get; set; }
        public string InnerException { get; set; }
        public string StackTrace { get; set; }
    }
}