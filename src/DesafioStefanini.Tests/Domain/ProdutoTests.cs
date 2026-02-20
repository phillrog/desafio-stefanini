using DesafioStefanini.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace DesafioStefanini.Tests.Domain;

public class ProdutoTests
{
    [Fact]
    [Trait("Category", "Domain")]
    public void Produto_DeveSerCriadoComSucesso_QuandoDadosValidos()
    {
        // Arrange
        var nome = "Mouse Gamer";
        var valor = 150.00m;

        // Act
        var produto = new Produto(nome, valor);

        // Assert
        produto.NomeProduto.Should().Be(nome);
        produto.Valor.Should().Be(valor);
    }
}