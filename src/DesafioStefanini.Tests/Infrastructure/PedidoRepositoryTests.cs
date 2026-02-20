using DesafioStefanini.Domain.Entities;
using DesafioStefanini.Infrastructure.Data.Contexts;
using DesafioStefanini.Infrastructure.Data.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace DesafioStefanini.Tests.Infrastructure;

public class PedidoRepositoryTests
{
    private ApplicationDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // DB único por teste
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    [Trait("Category", "Infrastructure")]
    public async Task GetPedidoCompletoAsync_DeveRetornarPedidoComItensEProdutos()
    {
        // Arrange
        using var context = GetDbContext();
        var repository = new PedidoRepository(context);

        // Criando dados para o teste
        var produto = new Produto("Mouse", 50.00m);
        context.Produtos.Add(produto);
        await context.SaveChangesAsync();

        var pedido = new Pedido("Phill", "phill@test.com", false);
        pedido.AdicionarItem(produto, 2);

        context.Pedidos.Add(pedido);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetPedidoCompletoAsync(pedido.Id);

        // Assert
        result.Should().NotBeNull();
        result!.NomeCliente.Should().Be("Phill");
        result.ItensPedido.Should().HaveCount(1);
        result.ItensPedido.First().Produto.NomeProduto.Should().Be("Mouse");
        result.ValorTotal.Should().Be(100.00m);
    }

    [Fact]
    [Trait("Category", "Infrastructure")]
    public async Task CreateAsync_DevePersistirPedidoNoBanco()
    {
        // Arrange
        using var context = GetDbContext();
        var repository = new PedidoRepository(context);
        var pedido = new Pedido("Stefanini", "vaga@stefanini.com", true);

        // Act
        await repository.CreateAsync(pedido);
        await repository.SaveChangesAsync();

        // Assert
        var pedidoNoBanco = await context.Pedidos.FindAsync(pedido.Id);
        pedidoNoBanco.Should().NotBeNull();
        pedidoNoBanco!.NomeCliente.Should().Be("Stefanini");
    }

    [Fact]
    [Trait("Category", "Infrastructure")]
    public async Task GetPedidoCompletoAsync_DeveRetornarNull_QuandoPedidoNaoExistir()
    {
        // Arrange
        using var context = GetDbContext();
        var repository = new PedidoRepository(context);

        // Act
        var result = await repository.GetPedidoCompletoAsync(999); // ID que não existe

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    [Trait("Category", "Infrastructure")]
    public async Task SaveChangesAsync_DeveLancarExcecao_QuandoNomeClienteForNulo()
    {
        // Arrange
        using var context = GetDbContext();
        var repository = new PedidoRepository(context);

        // Criando um pedido inválido (Violando a regra de 'Required' do Fluent API)
        var pedidoInvalido = new Pedido(null!, "email@email.com", false);
        await context.Pedidos.AddAsync(pedidoInvalido);

        // Act
        Func<Task> action = async () => await repository.SaveChangesAsync();

        // Assert
        await action.Should().ThrowAsync<DbUpdateException>();
    }
}