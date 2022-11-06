using CadastroMotorista.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroMotorista.Domain.Interfaces.Repositories
{
    public interface IRepositoryUsuario
    {
        Task<Usuario> GetUserById(int id);
    }
}
