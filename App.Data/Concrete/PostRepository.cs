using App.Data.Abstract;
using App.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Data.Concrete
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Post>> GetAllPostsByIncludeAsync()
        {
            var posts = await _context.Posts
                .Include(x => x.Comments)
                .AsNoTracking()
                .ToListAsync();

            // Her bir post için comments sayısını hesaplayıp CommentsCount özelliğini güncelliyoruz
            foreach (var post in posts)
            {
                post.CommentsCount = post.Comments?.Count ?? 0;
            }

            return posts;
        }

        public async Task<Post> GetPostByIncludeAsync(int id)
        {
            var post = await _context.Posts
                .Include(x => x.Comments)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            // Comments sayısını hesaplayıp CommentsCount özelliğini güncelliyoruz
            post.CommentsCount = post.Comments?.Count ?? 0;

            return post;
        }

        public async Task<List<Post>> GetSomePostsByIncludeAsync(Expression<Func<Post, bool>> expression)
        {
            var posts = await _context.Posts
                .Include(p => p.Comments)
                .AsNoTracking()
                .Where(expression)
                .ToListAsync();

            // Her bir post için comments sayısını hesaplayıp CommentsCount özelliğini güncelliyoruz
            foreach (var post in posts)
            {
                post.CommentsCount = post.Comments?.Count ?? 0;
            }

            return posts;
        }
    }
}
