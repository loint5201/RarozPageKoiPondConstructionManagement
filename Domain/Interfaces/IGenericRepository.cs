namespace Domain.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        Task DeleteAsync(T entity);
    }
}
