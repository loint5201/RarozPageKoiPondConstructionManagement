using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore; // Đảm bảo bạn đã thêm using này
using System.Linq.Expressions;

namespace Infrastructure.Persistence
{
    public class BaseRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly KoiPondConstructionManagementContext _context;

        public BaseRepository(KoiPondConstructionManagementContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync(); // Lưu thay đổi
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(); // Lưu thay đổi
        }

        public async Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> predicate = null,
                                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                        int? skip = null,
                                                        int? take = null,
                                                        bool disableTracking = true,
                                                        params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            // Disable tracking if specified
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            // Apply includes
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            // Apply predicate (filtering)
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            // Apply ordering
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            // Apply pagination
            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            // Execute query asynchronously
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id); // Sử dụng FindAsync để tìm thực thể
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync(); // Lưu thay đổi
        }
    }
}
