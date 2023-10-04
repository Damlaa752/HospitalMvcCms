using App.Data;
using App.Data.Concrete;
using App.Service.Abstract;

namespace App.Service.Concrete
{
    public class PatientService : PatientRepository, IPatientService
    {
        public PatientService(AppDbContext context) : base(context)
        {
        }
    }
}
