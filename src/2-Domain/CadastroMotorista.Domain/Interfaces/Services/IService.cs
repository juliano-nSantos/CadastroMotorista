using CadastroMotorista.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroMotorista.Domain.Interfaces.Services
{
    public interface IService<T>
    {
        public string MensagemErro { get; set; }
        Task<bool> AdicionarAsync(T entity);
        Task<bool> AtualizarAsync(T entity);
        Task<bool> DeletarAsync(int id);
        Task<List<T>> BuscarPorFiltroAsync(FiltroBusca filtroBusca);
        Task<T> BuscarPorCodigoAsync(int id);
    }
}
