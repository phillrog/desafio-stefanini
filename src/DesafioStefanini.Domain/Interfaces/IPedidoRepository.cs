using DesafioStefanini.Domain.Entities;

namespace DesafioStefanini.Domain.Interfaces
{
    public interface IPedidoRepository : IRepositoryBase<Pedido>
    {
        Task<Pedido?> GetPedidoCompletoAsync(int id);
    }
}
