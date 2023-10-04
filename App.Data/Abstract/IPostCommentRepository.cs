using App.Data.Entity;
using System.Linq.Expressions;

namespace App.Data.Abstract
{
    public interface IPostCommentRepository : IRepository<PostComment>
    {
        Task<PostComment> GetPostCommentByIncludeAsync(int id);

        Task<List<PostComment>> GetAllPostCommentsByIncludeAsync();

        Task<List<PostComment>> GetSomePostCommentsByIncludeAsync(Expression<Func<PostComment, bool>> expression);
    }
}
