using DesafioStefanini.Application.DTOs;
using DesafioStefanini.Application.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace DesafioStefanini.Tests.Application.Validators;

public class PedidoValidatorTests
{
    private readonly CreatePedidoValidator _createValidator;
    private readonly UpdatePedidoValidator _updateValidator;

    public PedidoValidatorTests()
    {
        _createValidator = new CreatePedidoValidator();
        _updateValidator = new UpdatePedidoValidator();
    }

    #region CreatePedidoValidator Tests

    [Fact]
    [Trait("Category", "Validator")]
    public void CreateValidator_DeveTerErro_QuandoDadosForemVazios()
    {
        var request = new CreatePedidoRequest("", "", new List<CreateItemPedidoRequest>());

        var result = _createValidator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.NomeCliente);
        result.ShouldHaveValidationErrorFor(x => x.EmailCliente);
        result.ShouldHaveValidationErrorFor(x => x.Itens);
    }

    [Fact]
    [Trait("Category", "Validator")]
    public void CreateValidator_DeveTerErro_QuandoItemPossuirQuantidadeInvalida()
    {
        var request = new CreatePedidoRequest("Phill", "phill@test.com", new List<CreateItemPedidoRequest>
        {
            new(1, 0) // Quantidade zero
        });

        var result = _createValidator.TestValidate(request);

        // Valida o erro dentro da coleção
        result.ShouldHaveAnyValidationError();
    }

    #endregion

    #region UpdatePedidoValidator Tests

    [Fact]
    [Trait("Category", "Validator")]
    public void UpdateValidator_DeveTerErro_QuandoIdForZeroOuNegativo()
    {
        var request = new UpdatePedidoRequest(0, "Phill", "phill@test.com", false, new List<UpdateItemPedidoRequest>());

        var result = _updateValidator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.Id)
              .WithErrorMessage("ID do pedido é obrigatório para atualização.");
    }

    [Fact]
    [Trait("Category", "Validator")]
    public void UpdateValidator_DevePassar_QuandoDadosForemValidos()
    {
        var request = new UpdatePedidoRequest(1, "Phill", "phill@test.com", false, new List<UpdateItemPedidoRequest>
        {
            new(10, 5)
        });

        var result = _updateValidator.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    #endregion
}