using Ecommerce.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Ecommerce.Data;
using System.Linq.Expressions;

namespace Ecommerce.Repositories
{
    public class GenericRepository<T> : IGenericRespository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) => await _dbSet.Where(predicate).ToListAsync();

        public async Task<bool> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public bool Update(T entity)
        {
            _dbSet.Update(entity);
            return true;
        }

        public bool Delete(T entity)
        {
            _dbSet.Remove(entity);
            return true;
        }

    }   
}
