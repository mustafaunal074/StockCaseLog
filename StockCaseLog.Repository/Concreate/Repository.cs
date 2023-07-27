using Microsoft.EntityFrameworkCore;
using StockCaseLog.Repository.Abstract;
using StockCaseLog.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StockCaseLog.Repository.Concreate
{
    public class Service<T> : IRepository<T> where T : class
    {
        private readonly StockCaseLogDbContext _context;

        public Service(StockCaseLogDbContext context)
        {
            _context = context;

        }

        public DbSet<T> Table => _context.Set<T>();

        public Task AddAsync(T entity)
        {
            Table.AddAsync(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task AddRangeAsync(IEnumerable<T> entities)
        {
            Table.AddRangeAsync(entities);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Table.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await Table.FindAsync(id);
        }

        public void Remove(T entity)
        {
            Table.Remove(entity);
            _context.SaveChangesAsync();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Table.RemoveRange(entities);
            _context.SaveChanges();
        }

        public async Task<bool> SingleOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await Table.AnyAsync(expression);
        }

        public void Update(T entity)
        {
            Table.Update(entity);
            _context.SaveChanges();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return Table.Where(expression);
        }
    }
}
