using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vendas.Domain;

namespace Vendas.Infrastructure.EntityConfigurations
{
    class CondPagtoEntityTypeConfiguration : IEntityTypeConfiguration<CondPagto>
    {
        public void Configure(EntityTypeBuilder<CondPagto> model)
        {
            model.ToTable("condpagto");
            model.HasKey(c => c.Id);

            model.Property(b => b.Id)
                .IsRequired()
                .HasColumnName("cpg-id");

            model.Property(b => b.Descricao)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("cpg-descricao")
                .HasColumnType("varchar(45)");

            model.Property(b => b.Dias)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("cpg-dias")
                .HasColumnType("varchar(45)");
        }
    }
}
