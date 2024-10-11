using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore; // Đảm bảo bạn đã thêm using này

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

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync(); // Sử dụng ToListAsync để lấy dữ liệu
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
