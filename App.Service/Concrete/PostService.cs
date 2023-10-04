using App.Data;
using App.Data.Concrete;
using App.Service.Abstract;

namespace App.Service.Concrete
{
    public class PostService : PostRepository, IPostService
    {
        public PostService(AppDbContext context) : base(context)
        {
        }
    }
}
