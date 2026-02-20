using DesafioStefanini.Domain.Entities;
using DesafioStefanini.Domain.Interfaces;
using DesafioStefanini.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DesafioStefanini.Infrastructure.Data.Repositories
{
    public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
    {
        public PedidoRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Pedido?> GetPedidoCompletoAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.ItensPedido)
                .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
