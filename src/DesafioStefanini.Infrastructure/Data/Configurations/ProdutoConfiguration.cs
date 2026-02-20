using DesafioStefanini.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioStefanini.Infrastructure.Data.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.NomeProduto)
                .HasMaxLength(20)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(p => p.Valor)
                .HasPrecision(10, 2)
                .IsRequired();
        }
    }
}
