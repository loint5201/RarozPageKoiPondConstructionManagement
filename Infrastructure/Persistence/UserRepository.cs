using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly KoiPondConstructionManagementContext _context;

        public UserRepository(KoiPondConstructionManagementContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> AnyAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.AnyAsync(predicate);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
