using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        // Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> predicate = null);
        // get all async has predicate, order, skip, take, include, 
        Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> predicate = null,
                                            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                            int? skip = null,
                                            int? take = null,
                                            bool disableTracking = true,
                                            params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
