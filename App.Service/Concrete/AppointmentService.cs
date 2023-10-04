using App.Data;
using App.Data.Concrete;
using App.Service.Abstract;

namespace App.Service.Concrete
{
    public class AppointmentService : AppointmentRepository, IAppointmentService
    {
        public AppointmentService(AppDbContext context) : base(context)
        {
        }
    }
}
