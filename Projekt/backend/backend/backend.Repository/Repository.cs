using backend.Data.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace backend.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly AppDbContext _context;
        private DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<bool> Exists(Expression<Func<T, bool>> func)
        {
            bool exists = await _dbSet.AnyAsync(func);

            return exists;
        }

        public async Task<T> GetEntity(Expression<Func<T, bool>> func)
        {
            var entity = await _dbSet.SingleOrDefaultAsync(func);

            return entity;
        }

        public async void Add(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async void Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async void Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetBy(Expression<Func<T, bool>> func)
        {
            var list = await _dbSet.Where(func).ToListAsync();

            return list;
        }

        public async Task<List<T>> GetAll()
        {
            var list = await _dbSet.ToListAsync();

            return list;
        }
    }
}
