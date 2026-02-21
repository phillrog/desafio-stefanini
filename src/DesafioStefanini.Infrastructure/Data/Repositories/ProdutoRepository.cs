using DesafioStefanini.Domain.Entities;
using DesafioStefanini.Domain.Interfaces.Repositories;
using DesafioStefanini.Infrastructure.Data.Contexts;

namespace DesafioStefanini.Infrastructure.Data.Repositories
{
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ApplicationDbContext context) : base(context) { }
    }
}
