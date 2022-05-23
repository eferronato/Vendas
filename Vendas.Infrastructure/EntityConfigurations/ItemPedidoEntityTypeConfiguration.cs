using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vendas.Domain;

namespace Vendas.Infrastructure.EntityConfigurations
{
    class ItemPedidoEntityTypeConfiguration : IEntityTypeConfiguration<ItemPedido>
    {
        public void Configure(EntityTypeBuilder<ItemPedido> model)
        {
            model.ToTable("itempedido");
            model.HasKey(c => c.Id);

            model.Property(b => b.Id)
                .IsRequired()
                .HasColumnName("itp-id");

            model.Property(b => b.PedidoId)
               .IsRequired()
               .HasColumnName("ped-id");

            model.Property(b => b.ProdutoId)
               .IsRequired()
               .HasColumnName("pro-id");

            model.Property(b => b.Quantidade)
                .IsRequired()
                .HasColumnName("itp-quantidade")
                .HasColumnType("decimal(18,4)");

            model.Property(b => b.Preco)
                .HasColumnName("itp-preco")
                .HasColumnType("decimal(18,2)");

            model.HasOne(x => x.Pedido)
                .WithMany(b => b.Itens)
                .HasForeignKey(x => x.PedidoId)
                .HasConstraintName("itp-ped-id-fk");

            model.HasOne(x => x.Produto)
                .WithMany()
                .HasForeignKey(x => x.ProdutoId)
                .HasConstraintName("itp-pro-id-fk");                            
        }
    }
}
