using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vendas.Domain;

namespace Vendas.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly VendasDbContext _context;

        public PedidoRepository(VendasDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Pedido>> GetAsync(int pageSize = 10, int pageIndex = 0)
        {
            var result = await _context.Pedidos
                .OrderByDescending(c => c.DataEmissao)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        public async Task<Pedido> GetByIdAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .AsNoTracking()
                .Include(p => p.CondPagto)
                .AsNoTracking()
                .Include(p => p.Itens)
                .AsNoTracking()
                .SingleOrDefaultAsync(ci => ci.Id == id);
        }

        public async Task<long> CountAsync()
        {
            return await _context.Pedidos.LongCountAsync();
        }

        public async Task<Pedido> AddAsync(Pedido entity)
        {
            _context.Pedidos.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Pedido> UpdateAsync(Pedido entity)
        {
            var result = await _context.Pedidos.SingleOrDefaultAsync(i => i.Id == entity.Id);
            if (result == null)
                return null;

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<decimal?> GetUltimoPrecoByClienteProduto(int clienteId, int produtoId)
        {
            var query = from item in _context.Set<ItemPedido>()
                        join pedido in _context.Set<Pedido>()
                          on item.PedidoId equals pedido.Id
                        where item.ProdutoId == produtoId
                           && pedido.ClienteId == clienteId
                        orderby pedido.DataEmissao descending
                        select (decimal?) item.Preco;

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Pedido>> GetPedidosByCnpjCliente(string cnpj, int pageSize = 50, int pageIndex = 0)
        {
            var query = from pedido in _context.Set<Pedido>()
                        join cliente in _context.Set<Cliente>()
                          on pedido.ClienteId equals cliente.Id
                        where cliente.Cnpj == cnpj
                        orderby pedido.DataEmissao descending
                        select pedido;

            return await ExecuteQueryForReport(query, pageSize, pageIndex);
        }

        public async Task<IEnumerable<Pedido>> GetPedidosByRazaoSocialCliente(string razaoSocial, int pageSize = 50, int pageIndex = 0)
        {
            var query = from pedido in _context.Set<Pedido>()
                        join cliente in _context.Set<Cliente>()
                          on pedido.ClienteId equals cliente.Id
                        where cliente.RazaoSocial == razaoSocial
                        orderby pedido.DataEmissao descending
                        select pedido;

            return await ExecuteQueryForReport(query, pageSize, pageIndex);
        }

        private async Task<IEnumerable<Pedido>> ExecuteQueryForReport(IQueryable<Pedido> query, int pageSize = 50, int pageIndex = 0)
        {
            return await query
                .Include(p => p.Cliente).AsNoTracking()
                .Include(p => p.CondPagto).AsNoTracking()
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto).AsNoTracking()
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
