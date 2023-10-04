using App.Data.Entity;
using System.Linq.Expressions;

namespace App.Data.Abstract
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<Patient> GetPatientByIncludeAsync(int id);

        Task<List<Patient>> GetAllPatientsByIncludeAsync();

        Task<List<Patient>> GetSomePatientsByIncludeAsync(Expression<Func<Patient, bool>> expression);
    }
}
