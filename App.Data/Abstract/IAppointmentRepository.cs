using App.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Abstract
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<Appointment> GetAppointmentByIncludeAsync(int id);

        Task<List<Appointment>> GetAllAppointmentsByIncludeAsync();

        Task<List<Appointment>> GetSomeAppointmentsByIncludeAsync(Expression<Func<Appointment, bool>>expression);

    }
}
