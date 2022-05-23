using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vendas.Domain;

namespace Vendas.Infrastructure.Repositories
{
    public class PoliticaPrecoRepository : IPoliticaPrecoRepository
    {
        private readonly VendasDbContext _context;

        public PoliticaPrecoRepository(VendasDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<PoliticaPreco>> GetAsync(int pageSize = 10, int pageIndex = 0)
        {
            var result = await _context.PoliticasPreco.AsNoTracking()
                .OrderBy(c => c.Descricao)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .Include(p => p.Cliente)
                .AsNoTracking()
                .Include(p => p.Produto)
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        public async Task<PoliticaPreco> GetByIdAsync(int id)
        {
            return await _context.PoliticasPreco.AsNoTracking()
                .Include(p => p.Cliente)
                .AsNoTracking()
                .Include(p => p.Produto)
                .AsNoTracking()
                .SingleOrDefaultAsync(ci => ci.Id == id);
        }

        public async Task<long> CountAsync()
        {
            return await _context.PoliticasPreco.LongCountAsync();
        }

        public async Task<PoliticaPreco> AddAsync(PoliticaPreco entity)
        {
            _context.PoliticasPreco.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PoliticaPreco> UpdateAsync(PoliticaPreco entity)
        {                        
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = _context.PoliticasPreco.SingleOrDefault(x => x.Id == id);
            if (entity == null)
                return false;

            _context.PoliticasPreco.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
