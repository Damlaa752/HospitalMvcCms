using App.Data;
using App.Data.Concrete;
using App.Service.Abstract;

namespace App.Service.Concrete
{
    public class DoctorsService : DoctorsRepository, IDoctorsService
    {
        public DoctorsService(AppDbContext context) : base(context)
        {
        }
    }
}
