using DesafioStefanini.Domain.Entities;
using FluentAssertions;

namespace DesafioStefanini.Tests.Domain;

public class PedidoTests
{
    [Fact]
    [Trait("Category", "Domain")]
    public void Pedido_DeveCalcularValorTotalCorretamente_QuandoPossuirItens()
    {
        // Arrange
        var pedido = new Pedido("Phill", "phill@email.com", false);
        var produto1 = new Produto("Teclado", 100.00m);
        var produto2 = new Produto("Mouse", 50.00m);

        // Act
        pedido.AdicionarItem(produto1, 2); // 200.00
        pedido.AdicionarItem(produto2, 1); // 50.00

        // Assert
        // O valor total deve ser 250.00 conforme regra (Quantidade * ValorUnitario)
        pedido.ValorTotal.Should().Be(250.00m);
    }

    [Fact]
    [Trait("Category", "Domain")]
    public void Pedido_DeveTerValorTotalZero_QuandoNaoPossuirItens()
    {
        // Arrange
        var pedido = new Pedido("Stefanini", "candidato@stefanini.com", false);

        // Act & Assert
        pedido.ValorTotal.Should().Be(0);
    }
}