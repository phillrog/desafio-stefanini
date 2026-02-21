using DesafioStefanini.Application.DTOs;
using DesafioStefanini.Domain.Common.Models;

namespace DesafioStefanini.Application.Interfaces.Services
{
    public interface IProdutoService
    {
        Task<Result<IEnumerable<ProdutoResponse>>> GetAllAsync();
    }
}
