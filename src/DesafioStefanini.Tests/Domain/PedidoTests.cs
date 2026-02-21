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
        pedido.ValorTotal.Should().Be(250.00m);
    }

    [Fact]
    [Trait("Category", "Domain")]
    public void AtualizarDados_DeveRetornarSucesso_QuandoPedidoNaoEstiverPago()
    {
        // Arrange
        var pedido = new Pedido("Antigo Nome", "antigo@email.com", false);

        // Act
        var result = pedido.AtualizarDados("Novo Nome", "novo@email.com", false);

        // Assert
        result.IsSuccess.Should().BeTrue();
        pedido.NomeCliente.Should().Be("Novo Nome");
    }

    [Fact]
    [Trait("Category", "Domain")]
    public void AtualizarDados_DeveRetornarFalha_QuandoTentarAlterarPedidoJaPago()
    {
        // Arrange
        var pedido = new Pedido("Phill", "phill@email.com", true);

        // Act
        var result = pedido.AtualizarDados("Novo Nome", "novo@email.com", true);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain("Este pedido já foi finalizado/pago e não permite alterações.");
    }

    [Fact]
    [Trait("Category", "Domain")]
    public void AtualizarDados_DeveRetornarFalha_QuandoTentarReverterPagamento()
    {
        // Arrange
        var pedido = new Pedido("Phill", "phill@email.com", true);

        // Act
        var result = pedido.AtualizarDados("Phill", "phill@email.com", false);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain("Não é possível reverter um pedido pago para não pago.");
    }

    [Fact]
    [Trait("Category", "Domain")]
    public void AtualizarItens_DeveRetornarFalha_QuandoPedidoEstiverPago()
    {
        // Arrange
        var pedido = new Pedido("Phill", "phill@email.com", true);
        var novosItens = new List<ItemPedido>();

        // Act
        var result = pedido.AtualizarItens(novosItens);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain("Não é possível alterar itens de um pedido que já foi pago.");
    }

    [Fact]
    [Trait("Category", "Domain")]
    public void AdicionarItem_DeveRetornarFalha_QuandoProdutoForInvalido()
    {
        // Arrange
        var pedido = new Pedido("Phill", "phill@email.com", false);

        // Act - Tentando adicionar com quantidade zero (regra da Factory do ItemPedido)
        var result = pedido.AdicionarItem(new Produto("Teste", 10), 0);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain("A quantidade deve ser maior que zero.");
    }
}