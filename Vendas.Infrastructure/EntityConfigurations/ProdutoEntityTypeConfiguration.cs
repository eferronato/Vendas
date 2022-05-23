using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vendas.Domain;

namespace Vendas.Infrastructure.EntityConfigurations
{
    class ProdutoEntityTypeConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> model)
        {
            model.ToTable("produto");
            model.HasKey(c => c.Id);

            model.Property(b => b.Id)
                .IsRequired()
                .HasColumnName("pro-id");

            model.Property(b => b.SKU)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("pro-SKU")
                .HasColumnType("varchar(45)");

            model.Property(b => b.Descricao)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("pro-descricao")
                .HasColumnType("varchar(45)");
        }
    }
}
