using App.Data.Abstract;
using App.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Concrete
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<User>> GetAllUserByIncludeAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .ToListAsync();
        }

        public async Task<User> GetUserByIncludeAsync(int userId)
        {
            return await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Role)
                .FirstOrDefaultAsync();
        }
    }
}

