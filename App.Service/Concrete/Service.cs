using App.Data;
using App.Data.Concrete;
using App.Data.Entity.BaseEntities;
using App.Service.Abstract;

namespace App.Service.Concrete
{
    public class Service<T> : Repository<T>, IService<T> where T : class, IEntity, new()
    {
        public Service(AppDbContext context) : base(context)
        {

        }
    }
}
