using DesafioStefanini.Application.DTOs;
using DesafioStefanini.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioStefanini.API.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        /// <summary>
        /// Obtém a listagem de todos os produtos disponíveis para venda.
        /// </summary>
        /// <response code="200">Retorna a lista de produtos.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProdutoResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _produtoService.GetAllAsync();
            return Ok(result);
        }
    }
}
