using DesafioStefanini.Domain.Entities;
using DesafioStefanini.Domain.Interfaces;
using DesafioStefanini.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DesafioStefanini.Infrastructure.Data.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public RepositoryBase(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        public async Task<T?> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);

        public async Task CreateAsync(T entity) => await _context.Set<T>().AddAsync(entity);

        public void Update(T entity) => _context.Set<T>().Update(entity);

        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }    
}
