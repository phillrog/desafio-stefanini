using DesafioStefanini.Application.DTOs;
using DesafioStefanini.Application.Interfaces.Services;
using DesafioStefanini.Domain.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioStefanini.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PedidoController : ControllerBase
{
    #region [ VARIÁVEIS PRIVADAS ]

    private readonly IPedidoService _pedidoService;

    #endregion

    #region [ CONSTRUTOR ]

    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }
    #endregion

    #region [ GET ]


    /// <summary>
    /// Obtém a listagem completa de todos os pedidos cadastrados.
    /// </summary>
    /// <response code="200">Retorna a lista de pedidos com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PedidoResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _pedidoService.GetAllAsync();
        return Ok(result.Data);
    }

    /// <summary>
    /// Obtém os detalhes de um pedido específico através do seu ID.
    /// </summary>
    /// <remarks>
    /// Retorna o cabeçalho do pedido, seus itens vinculados e o cálculo do valor total.
    /// </remarks>
    /// <param name="id">Identificador único do pedido.</param>
    /// <response code="200">Pedido encontrado com sucesso.</response>
    /// <response code="404">Pedido não encontrado no banco de dados.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _pedidoService.GetByIdAsync(id);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result.Data);
    }
    #endregion

    #region [ POST ]


    /// <summary>
    /// Cria um novo pedido no sistema.
    /// </summary>
    /// <remarks>
    /// O pedido é persistido no banco de dados com status de pagamento pendente (Pago = false).
    /// O valor total é calculado automaticamente com base nos itens enviados.
    /// </remarks>
    /// <param name="request">Dados necessários para a criação do pedido e seus itens.</param>
    /// <response code="201">Pedido criado com sucesso.</response>
    /// <response code="400">Dados inválidos ou falha na validação das regras de negócio.</response>
    [HttpPost]
    [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePedidoRequest request)
    {
        var result = await _pedidoService.CreateAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result);

        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
    }
    #endregion

    #region [ PUT ]


    /// <summary>
    /// Atualiza os dados de um pedido existente.
    /// </summary>
    /// <remarks>
    /// Só é permitido atualizar pedidos que ainda não foram marcados como "Pago".
    /// Esta operação substitui a lista de itens anterior pela nova lista enviada.
    /// </remarks>
    /// <param name="id">ID do pedido a ser atualizado.</param>
    /// <param name="request">Dados atualizados do pedido.</param>
    /// <response code="200">Pedido atualizado com sucesso.</response>
    /// <response code="400">Inconsistência de IDs ou violação de regra de negócio (Ex: Pedido já pago).</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePedidoRequest request)
    {
        if (id != request.Id)
            return BadRequest(Result.Failure("O ID da URL não coincide com o ID do corpo da requisição."));

        var result = await _pedidoService.UpdateAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result.Data);
    }
    #endregion

    #region [ DELETE ]


    /// <summary>
    /// Remove um pedido do sistema.
    /// </summary>
    /// <remarks>
    /// A exclusão só é permitida se o pedido não estiver pago. 
    /// Esta operação remove permanentemente o pedido e todos os itens associados.
    /// </remarks>
    /// <param name="id">ID do pedido a ser removido.</param>
    /// <response code="204">Pedido removido com sucesso.</response>
    /// <response code="400">Falha ao tentar remover um pedido pago ou inexistente.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _pedidoService.DeleteAsync(id);

        if (!result.IsSuccess)
            return BadRequest(result);

        return NoContent();
    }
    #endregion
}