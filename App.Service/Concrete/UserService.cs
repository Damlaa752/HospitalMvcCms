using App.Data;
using App.Data.Concrete;
using App.Service.Abstract;

namespace App.Service.Concrete
{
    public class UserService : UserRepository, IUserService
    {
        public UserService(AppDbContext context) : base(context)
        {
        }
    }
}
