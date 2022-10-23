using CadastroMotorista.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroMotorista.Domain.Interfaces.Services
{
    public interface IServiceLogin
    {
        Task<UsuarioDto> ValidarLogin(LoginDto login);
    }
}
