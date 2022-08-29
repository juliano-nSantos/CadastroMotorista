using CadastroMotorista.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroMotorista.Domain.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        Task<bool> AdicionarAsync(T entity);
        Task<bool> AtualizarAsync(T entity);
        Task<bool> DeletarAsync(int id);
        Task<List<T>> BuscarPorFiltroAsync(FiltroBusca filtroBusca);
        Task<T> BuscarPorCodigoAsync(int id);
    }
}
