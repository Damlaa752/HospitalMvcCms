using App.Data.Abstract;
using App.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Concrete
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Appointment>> GetAllAppointmentsByIncludeAsync()
        {
            return await _context.Appointments.Include(a => a.Doctor).Include(a => a.Department).ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByIncludeAsync(int id)
        {
            return await _context.Appointments.Include(a => a.Doctor).Include(a => a.Department).FirstOrDefaultAsync(a=> a.Id == id);
        }

        public async Task<List<Appointment>> GetSomeAppointmentsByIncludeAsync(Expression<Func<Appointment, bool>> expression)
        {
            return await _context.Appointments.Where(expression).Include(a => a.Doctor).Include(a => a.Department).ToListAsync();
        }
    }
}
