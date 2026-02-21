using DesafioStefanini.Domain.Entities;
using DesafioStefanini.Domain.Interfaces.Repositories;
using DesafioStefanini.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DesafioStefanini.Infrastructure.Data.Repositories
{
    public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
    {
        public PedidoRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            return await _context.Pedidos
                .Include(p => p.ItensPedido)
                .ThenInclude(i => i.Produto)
                .AsNoTracking()
                .OrderByDescending(d => d.DataCriacao)
                .ToListAsync();
        }
        public async Task<Pedido?> GetPedidoCompletoAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.ItensPedido)
                .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
