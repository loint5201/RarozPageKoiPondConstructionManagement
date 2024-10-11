using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<bool> AnyAsync(Expression<Func<User, bool>> predicate);
        void Add(User user);
        Task SaveChangesAsync();
    }
}
