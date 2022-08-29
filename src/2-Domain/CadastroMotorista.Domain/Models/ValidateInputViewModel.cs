using System.Collections.Generic;

namespace CadastroMotorista.Domain.Models
{
    public class ValidateInputViewModel
    {
        public IEnumerable<string> Errors { get; private set;}

        public ValidateInputViewModel(IEnumerable<string> _errors)
        {
            Errors = _errors;
        }
    }
}