using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vendas.Domain;

namespace Vendas.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly VendasDbContext _context;

        public ProdutoRepository(VendasDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Produto>> GetAsync(int pageSize = 10, int pageIndex = 0)
        {
            var result = await _context.Produtos
                .OrderBy(c => c.Descricao)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return result;
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            return await _context.Produtos.SingleOrDefaultAsync(ci => ci.Id == id);
        }

        public async Task<long> CountAsync()
        {
            return await _context.Produtos.LongCountAsync();
        }

        public async Task<Produto> AddAsync(Produto entity)
        {
            _context.Produtos.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Produto> UpdateAsync(Produto model)
        {
            var entity = await _context.Produtos.SingleOrDefaultAsync(i => i.Id == model.Id);

            if (entity == null)
                return null;

            entity.SKU = model.SKU;
            entity.Descricao = model.Descricao;
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = _context.Produtos.SingleOrDefault(x => x.Id == id);
            if (entity == null)
                return false;

            _context.Produtos.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
