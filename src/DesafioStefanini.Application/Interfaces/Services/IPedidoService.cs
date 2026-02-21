using DesafioStefanini.Application.DTOs;
using DesafioStefanini.Domain.Common.Models;

namespace DesafioStefanini.Application.Interfaces.Services
{
    public interface IPedidoService
    {
        Task<Result<PedidoResponse>> GetByIdAsync(int id);
        Task<Result<IEnumerable<PedidoResponse>>> GetAllAsync();
        Task<Result<PedidoResponse>> CreateAsync(CreatePedidoRequest request);
        Task<Result<PedidoResponse>> UpdateAsync(UpdatePedidoRequest request);
        Task<Result> DeleteAsync(int id);
    }
}
