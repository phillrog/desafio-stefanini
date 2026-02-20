using DesafioStefanini.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioStefanini.Infrastructure.Data.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedido");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.NomeCliente)
                .HasMaxLength(60)
                .IsRequired()
                .HasColumnType("varchar(60)");

            builder.Property(p => p.EmailCliente)
                .HasMaxLength(60)
                .IsRequired()
                .HasColumnType("varchar(60)");

            builder.Property(p => p.DataCriacao)
                .IsRequired();

            builder.Property(p => p.Pago)
                .HasColumnType("bit")
                .IsRequired();

            // Relacionamento 1:N
            builder.HasMany(p => p.ItensPedido)
                .WithOne(i => i.Pedido)
                .HasForeignKey(i => i.IdPedido)
                .OnDelete(DeleteBehavior.Cascade);

            // Ignora a propriedade calculada no Banco de Dados
            builder.Ignore(p => p.ValorTotal);
        }
    }
}
