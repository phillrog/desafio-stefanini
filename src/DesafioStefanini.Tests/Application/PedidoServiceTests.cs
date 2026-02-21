using Moq;
using FluentAssertions;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using DesafioStefanini.Application.Services;
using DesafioStefanini.Application.DTOs;
using DesafioStefanini.Domain.Interfaces.Repositories;
using DesafioStefanini.Domain.Entities;

namespace DesafioStefanini.Tests.Application;

public class PedidoServiceTests
{
    private readonly Mock<IPedidoRepository> _pedidoRepoMock;
    private readonly Mock<IProdutoRepository> _produtoRepoMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IValidator<CreatePedidoRequest>> _createValidatorMock;
    private readonly Mock<IValidator<UpdatePedidoRequest>> _updateValidatorMock;
    private readonly PedidoService _service;

    public PedidoServiceTests()
    {
        _pedidoRepoMock = new Mock<IPedidoRepository>();
        _produtoRepoMock = new Mock<IProdutoRepository>();
        _mapperMock = new Mock<IMapper>();
        _createValidatorMock = new Mock<IValidator<CreatePedidoRequest>>();
        _updateValidatorMock = new Mock<IValidator<UpdatePedidoRequest>>();

        _service = new PedidoService(
            _pedidoRepoMock.Object,
            _produtoRepoMock.Object,
            _mapperMock.Object,
            _createValidatorMock.Object,
            _updateValidatorMock.Object);
    }

    #region [ CENÁRIOS DE SUCESSO ]

    [Fact]
    [Trait("Category", "Service")]
    public async Task CreateAsync_DeveRetornarSucesso_QuandoDadosForemValidos()
    {
        // Arrange
        var request = new CreatePedidoRequest("Phill", "phill@test.com", new List<CreateItemPedidoRequest>
        {
            new(1, 2)
        });
        var produto = new Produto("Produto 1", 10.0m);

        _createValidatorMock.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _produtoRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(produto);

        // Act
        var result = await _service.CreateAsync(request);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _pedidoRepoMock.Verify(r => r.CreateAsync(It.IsAny<Pedido>()), Times.Once);
        _pedidoRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    [Trait("Category", "Service")]
    public async Task UpdateAsync_DeveRetornarSucesso_QuandoAtualizacaoForValida()
    {
        // Arrange
        var request = new UpdatePedidoRequest(1, "Novo Nome", "novo@email.com", false, new List<UpdateItemPedidoRequest>
        {
            new(1, 5)
        });
        var pedidoExistente = new Pedido("Antigo", "antigo@email.com", false);
        var produto = new Produto("Produto 1", 20.0m);

        _updateValidatorMock.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _pedidoRepoMock.Setup(r => r.GetPedidoCompletoAsync(request.Id)).ReturnsAsync(pedidoExistente);
        _produtoRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(produto);

        // Act
        var result = await _service.UpdateAsync(request);

        // Assert
        result.IsSuccess.Should().BeTrue();
        pedidoExistente.NomeCliente.Should().Be("Novo Nome");
        _pedidoRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    #endregion

    #region [ CENÁRIOS DE FALHA ]

    [Fact]
    [Trait("Category", "Service")]
    public async Task CreateAsync_DeveRetornarFalha_QuandoValidatorFalhar()
    {
        // Arrange
        var request = new CreatePedidoRequest("", "", new List<CreateItemPedidoRequest>());
        var failures = new List<ValidationFailure> { new("NomeCliente", "Nome é obrigatório") };

        _createValidatorMock.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(failures));

        // Act
        var result = await _service.CreateAsync(request);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain("Nome é obrigatório");
        _pedidoRepoMock.Verify(r => r.CreateAsync(It.IsAny<Pedido>()), Times.Never);
    }

    [Fact]
    [Trait("Category", "Service")]
    public async Task CreateAsync_DeveRetornarFalha_QuandoProdutoNaoExistir()
    {
        // Arrange
        var request = new CreatePedidoRequest("Phill", "phill@test.com", new List<CreateItemPedidoRequest> { new(99, 1) });

        _createValidatorMock.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        _produtoRepoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Produto?)null);

        // Act
        var result = await _service.CreateAsync(request);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain("Produto 99 não encontrado.");
    }

    [Fact]
    [Trait("Category", "Service")]
    public async Task UpdateAsync_DeveRetornarFalha_QuandoPedidoJaEstiverPago()
    {
        // Arrange
        var request = new UpdatePedidoRequest(1, "Phill", "phill@test.com", false, new List<UpdateItemPedidoRequest>());
        var pedidoExistente = new Pedido("Phill", "phill@test.com", true); // Já está pago

        _updateValidatorMock.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _pedidoRepoMock.Setup(r => r.GetPedidoCompletoAsync(request.Id)).ReturnsAsync(pedidoExistente);

        // Act
        var result = await _service.UpdateAsync(request);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain("Não é possível reverter um pedido pago para não pago.");
    }

    [Fact]
    [Trait("Category", "Service")]
    public async Task DeleteAsync_DeveRetornarFalha_QuandoPedidoEstiverPago()
    {
        // Arrange
        var pedidoPago = new Pedido("Phill", "phill@email.com", true);
        _pedidoRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(pedidoPago);

        // Act
        var result = await _service.DeleteAsync(1);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain("Não é possível excluir um pedido que já foi pago.");
        _pedidoRepoMock.Verify(r => r.Delete(It.IsAny<Pedido>()), Times.Never);
    }

    [Fact]
    [Trait("Category", "Service")]
    public async Task GetByIdAsync_DeveRetornarFalha_QuandoPedidoNaoExistir()
    {
        // Arrange
        _pedidoRepoMock.Setup(r => r.GetPedidoCompletoAsync(It.IsAny<int>())).ReturnsAsync((Pedido?)null);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain("Pedido não encontrado.");
    }

    #endregion
}