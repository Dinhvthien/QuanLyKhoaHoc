using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Domain.Interfaces
{
    public interface IRepository<T>
    {
        public IQueryable<T> Query(Expression<Func<T, bool>> expression);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression);
        public Task<T?> FindByIdAsync(int id);
        public Task<T?> FindAsync(Expression<Func<T, bool>> expression);
        public Task AddAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task RemoveAsync(T entity);
        public Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    }
}
