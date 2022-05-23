using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vendas.Domain;

namespace Vendas.Infrastructure.Repositories
{
    public class CondPagtoRepository : ICondPagtoRepository
    {
        private readonly VendasDbContext _context;

        public CondPagtoRepository(VendasDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<CondPagto>> GetAsync(int pageSize = 10, int pageIndex = 0)
        {
            var result = await _context.CondPagtos
                .OrderBy(c => c.Descricao)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return result;
        }

        public async Task<CondPagto> GetByIdAsync(int id)
        {
            return await _context.CondPagtos.SingleOrDefaultAsync(ci => ci.Id == id);
        }

        public async Task<long> CountAsync()
        {
            return await _context.CondPagtos.LongCountAsync();
        }

        public async Task<CondPagto> AddAsync(CondPagto entity)
        {
            _context.CondPagtos.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<CondPagto> UpdateAsync(CondPagto model)
        {
            var entity = await _context.CondPagtos.SingleOrDefaultAsync(i => i.Id == model.Id);

            if (entity == null)
                return null;
            
            entity.Descricao = model.Descricao;
            entity.Dias = model.Dias;
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = _context.CondPagtos.SingleOrDefault(x => x.Id == id);
            if (entity == null)
                return false;

            _context.CondPagtos.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
