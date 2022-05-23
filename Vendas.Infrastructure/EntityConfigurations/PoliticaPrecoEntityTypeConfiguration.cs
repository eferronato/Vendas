using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vendas.Domain;

namespace Vendas.Infrastructure.EntityConfigurations
{
    class PoliticaPrecoEntityTypeConfiguration : IEntityTypeConfiguration<PoliticaPreco>
    {
        public void Configure(EntityTypeBuilder<PoliticaPreco> model)
        {
            model.ToTable("polpreco");
            model.HasKey(c => c.Id);

            model.Property(b => b.Id)
                .IsRequired()
                .HasColumnName("pol-id");

            model.Property(b => b.Descricao)
                .HasMaxLength(45)
                .HasColumnName("pol-descricao")
                .HasColumnType("varchar(45)");

            model.Property(b => b.Preco)
                .IsRequired()
                .HasColumnName("pol-preco")
                .HasColumnType("decimal(18,2)");

            model.Property(b => b.ProdutoId)
                .IsRequired()
                .HasColumnName("pro-id");

            model.Property(b => b.ClienteId)
                .IsRequired()
                .HasColumnName("cli-id");

            model.HasOne(x => x.Produto)
                .WithMany()
                .HasForeignKey(x => x.ProdutoId)
                .HasConstraintName("pol-pro-id-fk");

            model.HasOne(x => x.Cliente)
                .WithMany()
                .HasForeignKey(x => x.ClienteId)
                .HasConstraintName("pol-cli-id-fk");
        }
    }
}
