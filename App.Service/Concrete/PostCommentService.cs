using App.Data;
using App.Data.Concrete;
using App.Service.Abstract;

namespace App.Service.Concrete
{
    public class PostCommentService : PostCommentRepository, IPostCommentService
    {
        public PostCommentService(AppDbContext context) : base(context)
        {
        }
    }
}
