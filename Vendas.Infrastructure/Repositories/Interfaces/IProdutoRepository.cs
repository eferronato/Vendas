using System.Collections.Generic;
using System.Threading.Tasks;
using Vendas.Domain;

namespace Vendas.Infrastructure.Repositories
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAsync(int pageSize = 10, int pageIndex = 0);
        Task<Produto> GetByIdAsync(int id);
        Task<long> CountAsync();
        Task<Produto> AddAsync(Produto entity);
        Task<Produto> UpdateAsync(Produto entityToUpdate);
        Task<bool> DeleteAsync(int id);
    }
}
