using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public BaseService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
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
