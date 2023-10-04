using App.Data.Abstract;
using App.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Data.Concrete
{
    public class DoctorsRepository : Repository<Doctors>, IDoctorsRepository
    {
        public DoctorsRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Doctors>> GetAllDoctorsByIncludeAsync()
        {
            return await _context.Doctors.Include(d => d.Patients).Include(d => d.Department).Include(d => d.Role).ToListAsync();
        }

        public async Task<Doctors> GetDoctorByIncludeAsync(int id)
        {
            return await _context.Doctors.Include(d => d.Patients).Include(d => d.Department).Include(d => d.Role).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Doctors>> GetSomeDoctorsByIncludeAsync(Expression<Func<Doctors, bool>> expression)
        {
            return await _context.Doctors.Where(expression).Include(d => d.Patients).Include(d => d.Role).Include(d => d.Department).ToListAsync();
        }
    }
}
