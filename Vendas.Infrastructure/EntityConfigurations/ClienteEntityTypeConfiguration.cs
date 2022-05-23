using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vendas.Domain;

namespace Vendas.Infrastructure.EntityConfigurations
{
    class ClienteEntityTypeConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> model)
        {
            model.ToTable("cliente");
            model.HasKey(c => c.Id);

            model.Property(b => b.Id)
                .IsRequired()
                .HasColumnName("cli-id");

            model.Property(b => b.Cnpj)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("cli-cnpj")
                .HasColumnType("varchar(45)");

            model.Property(b => b.RazaoSocial)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("cli-razaosocial")
                .HasColumnType("varchar(45)");
        }
    }
}
