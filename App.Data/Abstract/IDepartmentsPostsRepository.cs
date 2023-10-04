using App.Data.Entity;
using System.Linq.Expressions;

namespace App.Data.Abstract
{
    public interface IDepartmentsPostsRepository : IRepository<DepartmentPost>
    {
        Task<List<DepartmentPost>> GetAllDepartmentPostByIncludeAsync();
        Task<DepartmentPost> GetDepartmentPostByIncludeAsync(int id);
        Task<List<DepartmentPost>> GetSomeDepartmentPostByIncludeAsync(Expression<Func<DepartmentPost, bool>> expression);

    }
}
