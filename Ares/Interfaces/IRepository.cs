using API.Entities;
using System.Linq.Expressions;

namespace API.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> where, string[] includeProperties);
        IQueryable<T> GetAll(string[] includeProperties);
        Task<T> GetSingle(Expression<Func<T, bool>> where);
        Task<T> GetSingle(Expression<Func<T, bool>> where, string[] includeProperties);
        T Add(T entity);
        void Update(T entity);
        void Update(IEnumerable<T> entity);
        void Delete(T entity);
        Task<int> SaveAsync();
    }
}
