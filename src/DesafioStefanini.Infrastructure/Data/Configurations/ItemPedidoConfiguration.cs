using DesafioStefanini.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioStefanini.Infrastructure.Data.Configurations
{
    public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
    {
        public void Configure(EntityTypeBuilder<ItemPedido> builder)
        {
            builder.ToTable("ItensPedido");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Quantidade).IsRequired();
            builder.Property(i => i.ValorUnitario).HasPrecision(10, 2).IsRequired();

            builder.HasOne(i => i.Produto)
                .WithMany()
                .HasForeignKey(i => i.IdProduto);
        }
    }
}
