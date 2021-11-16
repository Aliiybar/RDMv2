using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sign.Db.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetAsync(Guid id);
        Task<IList<T>> GetAllAsync();
        Task<IList<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Delete(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> SaveAsync();
    }
}
