using App.Data.Entity;
using System.Linq.Expressions;

namespace App.Data.Abstract
{
    public interface IDoctorsRepository : IRepository<Doctors>
    {
        Task<Doctors> GetDoctorByIncludeAsync(int id);

        Task<List<Doctors>> GetAllDoctorsByIncludeAsync();

        Task<List<Doctors>> GetSomeDoctorsByIncludeAsync(Expression<Func<Doctors, bool>> expression);
    }
}
