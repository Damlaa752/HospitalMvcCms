using App.Data.Abstract;
using App.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Data.Concrete
{
    public class PostCommentRepository : Repository<PostComment>, IPostCommentRepository
    {
        public PostCommentRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<List<PostComment>> GetAllPostCommentsByIncludeAsync()
        {
            return await _context.PostComments.Include(p => p.User).Include(p => p.Post).ToListAsync();
        }

        public async Task<PostComment> GetPostCommentByIncludeAsync(int id)
        {
            return await _context.PostComments.Include(p => p.User).Include(p => p.Post).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<PostComment>> GetSomePostCommentsByIncludeAsync(Expression<Func<PostComment, bool>> expression)
        {
            return await _context.PostComments.Where(expression).Include(p => p.Post).Include(p => p.User).ToListAsync();
        }
    }
}
