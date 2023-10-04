using App.Data;
using App.Data.Concrete;
using App.Service.Abstract;

namespace App.Service.Concrete
{
    public class DepartmentsPostsService : DepartmentsPostsRepository, IDepartmentsPostsService
    {
        public DepartmentsPostsService(AppDbContext context) : base(context)
        {
        }
    }
}
