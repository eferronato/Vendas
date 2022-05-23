using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vendas.Domain;

namespace Vendas.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly VendasDbContext _context;

        public ClienteRepository(VendasDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Cliente>> GetAsync(int pageSize = 10, int pageIndex = 0)
        {
            var result = await _context.Clientes
                .OrderBy(c => c.RazaoSocial)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return result;
        }

        public async Task<Cliente> GetByIdAsync(int id)
        {
            return await _context.Clientes.SingleOrDefaultAsync(ci => ci.Id == id);
        }

        public async Task<long> CountAsync()
        {
            return await _context.Clientes.LongCountAsync();
        }

        public async Task<Cliente> AddAsync(Cliente entity)
        {            
            _context.Clientes.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Cliente> UpdateAsync(Cliente entity)
        {            
            var result = await _context.PoliticasPreco.SingleOrDefaultAsync(i => i.Id == entity.Id);
            if (result == null)
                return null;

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = _context.Clientes.SingleOrDefault(x => x.Id == id);
            if (entity == null)
                return false;

            _context.Clientes.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
