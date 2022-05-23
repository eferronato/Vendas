using System.Collections.Generic;
using System.Threading.Tasks;
using Vendas.Domain;

namespace Vendas.Infrastructure.Repositories
{
    public interface ICondPagtoRepository
    {
        Task<IEnumerable<CondPagto>> GetAsync(int pageSize = 10, int pageIndex = 0);
        Task<CondPagto> GetByIdAsync(int id);
        Task<long> CountAsync();
        Task<CondPagto> AddAsync(CondPagto entity);
        Task<CondPagto> UpdateAsync(CondPagto entityToUpdate);
        Task<bool> DeleteAsync(int id);
    }
}
