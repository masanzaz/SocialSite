using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync<TProperty>(Expression<Func<T, TProperty>> include);

        Task<IEnumerable<T>> GetPagedReponseAsync(int pageNumber, int pageSize);

        Task<T> GetByIdAsync(object id);

        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        Task<int> CountAsync();
    }
}
