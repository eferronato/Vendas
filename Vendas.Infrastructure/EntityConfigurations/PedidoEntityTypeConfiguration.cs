using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vendas.Domain;

namespace Vendas.Infrastructure.EntityConfigurations
{
    class PedidoEntityTypeConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> model)
        {
            model.ToTable("pedido");
            model.HasKey(c => c.Id);

            model.Property(b => b.Id)
                .IsRequired()
                .HasColumnName("ped-id");

            model.Property(b => b.DataEmissao)
                .HasColumnName("ped-dtemis");            

            model.Property(b => b.ClienteId)
                .IsRequired()
                .HasColumnName("cli-id");

            model.Property(b => b.CondPagtoId)
                .IsRequired()
                .HasColumnName("cpg-id");

            model.HasOne(x => x.Cliente)
                .WithMany()
                .HasForeignKey(x => x.ClienteId)
                .HasConstraintName("ped-cli-id-fk");

            model.HasOne(x => x.CondPagto)
                .WithMany()
                .HasForeignKey(x => x.CondPagtoId)
                .HasConstraintName("ped-cpg-id-fk");

            model.HasMany(b => b.Itens)
                .WithOne();
        }
    }
}
