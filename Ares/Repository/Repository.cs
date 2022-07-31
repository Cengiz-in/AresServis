using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext _context;
        public DbSet<T> Table { get; set; }
        public Repository(DataContext context)
        {
            _context = context;
            Table = _context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return Table;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> where, string[] includeProperties)
        {
            return includeProperties.Aggregate(Table.Where(where), (query, path) => query.Include(path)).AsNoTracking();
        }

        public IQueryable<T> GetAll(string[] includeProperties)
        {
            return includeProperties.Aggregate(Table.Where(x => true), (query, path) => query.Include(path)).AsNoTracking();
        }

        public async Task<T> GetSingle(Expression<Func<T, bool>> where)
        {
            return await Table.Where(where).SingleAsync();
        }

        public async Task<T> GetSingle(Expression<Func<T, bool>> where, string[] includeProperties)
        {
            var result = includeProperties.Aggregate(Table.Where(where), (query, path) => query.Include(path)).AsNoTracking();
            return await result.SingleAsync();
        }


        public async Task<int> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public T Add(T entity)
        {
            return Table.Add(entity).Entity;
        }

        public void Update(T entity)
        {
            entity.UpdateDate = DateTime.Now;
            Table.Update(entity);
        }

        public void Update(IEnumerable<T> entity)
        {
            Table.UpdateRange(entity);
        }

        public void Delete(T entity)
        {
            Table.Remove(entity);
        }
    }
}