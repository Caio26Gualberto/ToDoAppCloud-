using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Infra.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ToDoDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ToDoDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetAllAsync()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
