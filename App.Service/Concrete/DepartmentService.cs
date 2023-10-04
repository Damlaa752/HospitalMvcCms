using App.Data;
using App.Data.Concrete;
using App.Service.Abstract;

namespace App.Service.Concrete
{
    public class DepartmentService : DepartmentRepository, IDepartmentService
    {
        public DepartmentService(AppDbContext context) : base(context)
        {
        }
    }
}
