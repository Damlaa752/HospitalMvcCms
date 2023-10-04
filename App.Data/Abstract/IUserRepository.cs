using App.Data.Entity;

namespace App.Data.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByIncludeAsync(int userId);
        Task<List<User>> GetAllUserByIncludeAsync();
    }
}
