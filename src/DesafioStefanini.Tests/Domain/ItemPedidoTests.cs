using DesafioStefanini.Domain.Entities;
using FluentAssertions;

namespace DesafioStefanini.Tests.Domain
{
    public class ItemPedidoTests
    {
        [Fact]
        [Trait("Category", "Domain")]
        public void ItemPedido_DeveCapturarValorUnitarioDoProduto_NoMomentoDaCriacao()
        {
            // Arrange
            var valorOriginal = 100.00m;
            var produto = new Produto("Teclado", valorOriginal);
            var pedido = new Pedido("Cliente Teste", "teste@email.com", false);

            // Act
            pedido.AdicionarItem(produto, 1);
            var item = pedido.ItensPedido.First();

            // Assert
            item.ValorUnitario.Should().Be(valorOriginal);
        }
    }
}
