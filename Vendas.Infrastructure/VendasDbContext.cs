using Microsoft.EntityFrameworkCore;
using Vendas.Domain;
using Vendas.Infrastructure.EntityConfigurations;

namespace Vendas.Infrastructure
{
    public class VendasDbContext : DbContext
    {
        public VendasDbContext(DbContextOptions<VendasDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<CondPagto> CondPagtos { get; set; }
        public DbSet<PoliticaPreco> PoliticasPreco { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CondPagtoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PoliticaPrecoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ItemPedidoEntityTypeConfiguration());
        }
    }
}
