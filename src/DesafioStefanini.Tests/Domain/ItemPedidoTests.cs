using DesafioStefanini.Domain.Entities;
using FluentAssertions;

namespace DesafioStefanini.Tests.Domain
{
    public class ItemPedidoTests
    {
        [Fact]
        [Trait("Category", "Domain")]
        public void Criar_DeveRetornarSucesso_QuandoDadosForemValidos()
        {
            // Arrange
            var produto = new Produto("Mouse", 50.00m);
            var quantidade = 5;

            // Act
            var result = ItemPedido.Criar(produto, quantidade);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.Produto.Should().Be(produto);
            result.Data.Quantidade.Should().Be(quantidade);
        }

        [Fact]
        [Trait("Category", "Domain")]
        public void Criar_DeveRetornarFalha_QuandoProdutoForNulo()
        {
            // Act
            var result = ItemPedido.Criar(null!, 1);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().Contain("O produto é obrigatório.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [Trait("Category", "Domain")]
        public void Criar_DeveRetornarFalha_QuandoQuantidadeForInvalida(int quantidadeInvalida)
        {
            // Arrange
            var produto = new Produto("Monitor", 800.00m);

            // Act
            var result = ItemPedido.Criar(produto, quantidadeInvalida);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().Contain("A quantidade deve ser maior que zero.");
        }

        [Fact]
        [Trait("Category", "Domain")]
        public void ItemPedido_DeveCapturarValorUnitarioDoProduto_NoMomentoDaCriacao()
        {
            // Arrange
            var valorOriginal = 100.00m;
            var produto = new Produto("Teclado", valorOriginal);

            // Act
            var result = ItemPedido.Criar(produto, 1);

            // Assert
            result.Data!.ValorUnitario.Should().Be(valorOriginal);
        }

        [Fact]
        [Trait("Category", "Domain")]
        public void CalcularValorItem_DeveRetornarMultiplicacao_DeQuantidadePorValorUnitario()
        {
            // Arrange
            var produto = new Produto("Cadeira Gamer", 1200.00m);
            var quantidade = 3;
            var valorEsperado = 3600.00m;

            // Act
            var item = ItemPedido.Criar(produto, quantidade).Data;

            // Assert
            item!.CalcularValorItem().Should().Be(valorEsperado);
        }
    }
}