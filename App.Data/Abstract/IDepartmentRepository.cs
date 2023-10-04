using App.Data.Entity;
using System.Linq.Expressions;

namespace App.Data.Abstract
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<Department> GetDepartmentByIncludeAsync(int id);

        Task<List<Department>> GetAllDepartmentsByIncludeAsync();

        Task<List<Department>> GetSomeDepartmentsByIncludeAsync(Expression<Func<Department, bool>> expression);
    }
}
