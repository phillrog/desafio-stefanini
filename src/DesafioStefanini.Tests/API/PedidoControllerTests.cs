using DesafioStefanini.API.Controllers;
using DesafioStefanini.Application.DTOs;
using DesafioStefanini.Application.Interfaces.Services;
using DesafioStefanini.Domain.Common.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DesafioStefanini.Tests.API.Controllers;

public class PedidoControllerTests
{
    private readonly Mock<IPedidoService> _serviceMock;
    private readonly PedidoController _controller;

    public PedidoControllerTests()
    {
        _serviceMock = new Mock<IPedidoService>();
        _controller = new PedidoController(_serviceMock.Object);
    }

    #region [ GET TESTS ]

    [Fact]
    [Trait("Category", "Controller")]
    public async Task GetAll_DeveRetornarOk_ComListaDePedidos()
    {
        // Arrange
        var pedidos = new List<PedidoResponse> {
            new(1, "Cliente 1", "email@teste.com", false, 100, new List<ItemPedidoResponse>())
        };
        _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(Result<IEnumerable<PedidoResponse>>.Success(pedidos));

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(Result<IEnumerable<PedidoResponse>>.Success(pedidos));
    }

    [Fact]
    [Trait("Category", "Controller")]
    public async Task GetById_DeveRetornarNotFound_QuandoPedidoNaoExistir()
    {
        // Arrange
        var idInexistente = 99;
        _serviceMock.Setup(s => s.GetByIdAsync(idInexistente))
            .ReturnsAsync(Result<PedidoResponse>.Failure("Pedido não encontrado."));

        // Act
        var result = await _controller.GetById(idInexistente);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
    }

    #endregion

    #region [ POST TESTS ]

    [Fact]
    [Trait("Category", "Controller")]
    public async Task Create_DeveRetornarCreated_QuandoSucesso()
    {
        // Arrange
        var request = new CreatePedidoRequest("Phill", "phill@test.com", new List<CreateItemPedidoRequest>());

        var expectedResponse = new PedidoResponse(1, "Phill", "phill@test.com", false, 0, new List<ItemPedidoResponse>());

        _serviceMock.Setup(s => s.CreateAsync(It.IsAny<CreatePedidoRequest>()))
                    .ReturnsAsync(Result<PedidoResponse>.Success(expectedResponse));

        // Act
        var actionResult = await _controller.Create(request);

        // Assert

        var createdResult = actionResult.Should().BeOfType<CreatedAtActionResult>().Subject;
        createdResult.RouteValues?["id"].Should().Be(1);
        createdResult.Value.Should().BeEquivalentTo(Result<PedidoResponse>.Success(expectedResponse));        
    }

    #endregion

    #region [ PUT TESTS ]

    [Fact]
    [Trait("Category", "Controller")]
    public async Task Update_DeveRetornarBadRequest_QuandoIdsNaoCoincidirem()
    {
        // Arrange
        var idUrl = 1;
        var request = new UpdatePedidoRequest(2, "Nome", "email", false, new List<UpdateItemPedidoRequest>());

        // Act
        var result = await _controller.Update(idUrl, request);

        // Assert
        var badRequest = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        var resultValue = badRequest.Value as Result;
        resultValue!.Errors.Should().Contain(e => e.Contains("não coincide"));
    }

    #endregion

    #region [ DELETE TESTS ]

    [Fact]
    [Trait("Category", "Controller")]
    public async Task Delete_DeveRetornarNoContent_QuandoSucesso()
    {
        // Arrange
        _serviceMock.Setup(s => s.DeleteAsync(1)).ReturnsAsync(Result.Success());

        // Act
        var result = await _controller.Delete(1);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    #endregion
}