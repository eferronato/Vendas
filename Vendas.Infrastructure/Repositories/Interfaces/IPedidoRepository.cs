using System.Collections.Generic;
using System.Threading.Tasks;
using Vendas.Domain;

namespace Vendas.Infrastructure.Repositories
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> GetAsync(int pageSize = 10, int pageIndex = 0);
        Task<Pedido> GetByIdAsync(int id);
        Task<long> CountAsync();
        Task<Pedido> AddAsync(Pedido entity);
        Task<Pedido> UpdateAsync(Pedido entityToUpdate);
        Task<decimal?> GetUltimoPrecoByClienteProduto(int clienteId, int produtoId);
        Task<IEnumerable<Pedido>> GetPedidosByCnpjCliente(string cnpj, int pageSize = 50, int pageIndex = 0);
        Task<IEnumerable<Pedido>> GetPedidosByRazaoSocialCliente(string razaoSocial, int pageSize = 50, int pageIndex = 0);
    }
}
