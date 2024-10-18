using Application.Interfaces;
using Domain.Interfaces;
using System.Linq.Expressions;

namespace Application.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public BaseService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> predicate = null,
                                            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                            int? skip = null,
                                            int? take = null,
                                            bool disableTracking = true,
                                            params Expression<Func<T, object>>[] includes)
        {
            return await _repository.GetByCondition(predicate, orderBy, skip, take, disableTracking, includes);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("Entity not found");
            }
            await _repository.DeleteAsync(entity);
        }
    }

}
