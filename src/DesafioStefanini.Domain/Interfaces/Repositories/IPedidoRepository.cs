using DesafioStefanini.Domain.Entities;

namespace DesafioStefanini.Domain.Interfaces.Repositories
{
    public interface IPedidoRepository : IRepositoryBase<Pedido>
    {
        Task<IEnumerable<Pedido>> GetAllAsync();
        Task<Pedido?> GetPedidoCompletoAsync(int id);
    }
}
