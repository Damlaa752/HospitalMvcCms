using App.Data.Abstract;
using App.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Data.Concrete
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Department>> GetAllDepartmentsByIncludeAsync()
        {
            return await _context.Departments.Include(d=> d.Doctors).ToListAsync();
        }

        public async Task<Department> GetDepartmentByIncludeAsync(int id)
        {
            return await _context.Departments.Include(d => d.Doctors).FirstOrDefaultAsync(d => d.Id == id);
        }

        public Task<List<Department>> GetSomeDepartmentsByIncludeAsync(Expression<Func<Department, bool>> expression)
        {
            return _context.Departments.Where(expression).Include(d => d.Doctors).ToListAsync();
        }
    }
}
