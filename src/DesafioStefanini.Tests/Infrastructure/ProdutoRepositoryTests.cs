using DesafioStefanini.Domain.Entities;
using DesafioStefanini.Infrastructure.Data.Contexts;
using DesafioStefanini.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;

namespace DesafioStefanini.Tests.Infrastructure;

public class ProdutoRepositoryTests
{
    private ApplicationDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    [Fact]
    [Trait("Category", "Infrastructure")]
    public async Task GetByIdAsync_DeveRetornarProduto_QuandoExistirNoBanco()
    {
        // Arrange
        using var context = GetDbContext();
        var repository = new ProdutoRepository(context);
        var produto = new Produto("Monitor Pro", 1200.00m);
        context.Produtos.Add(produto);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(produto.Id);

        // Assert
        result.Should().NotBeNull();
        result!.NomeProduto.Should().Be("Monitor Pro");
    }
}