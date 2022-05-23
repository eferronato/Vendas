using System.Collections.Generic;
using System.Threading.Tasks;
using Vendas.Domain;

namespace Vendas.Infrastructure.Repositories
{
    public interface IPoliticaPrecoRepository
    {
        Task<IEnumerable<PoliticaPreco>> GetAsync(int pageSize = 10, int pageIndex = 0);
        Task<PoliticaPreco> GetByIdAsync(int id);
        Task<long> CountAsync();
        Task<PoliticaPreco> AddAsync(PoliticaPreco entity);
        Task<PoliticaPreco> UpdateAsync(PoliticaPreco entityToUpdate);
        Task<bool> DeleteAsync(int id);
    }
}
