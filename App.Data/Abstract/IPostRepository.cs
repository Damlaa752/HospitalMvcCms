using App.Data.Entity;
using System.Linq.Expressions;

namespace App.Data.Abstract
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post> GetPostByIncludeAsync(int id);

        Task<List<Post>> GetAllPostsByIncludeAsync();

        Task<List<Post>> GetSomePostsByIncludeAsync(Expression<Func<Post, bool>> expression);
    }
}
