using System.Collections.Generic;
using System.Threading.Tasks;
using Vendas.Domain;

namespace Vendas.Infrastructure.Repositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAsync(int pageSize = 10, int pageIndex = 0);
        Task<Cliente> GetByIdAsync(int id);
        Task<long> CountAsync();
        Task<Cliente> AddAsync(Cliente entity);
        Task<Cliente> UpdateAsync(Cliente entity);
        Task<bool> DeleteAsync(int id);
    }
}
