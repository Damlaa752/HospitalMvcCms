using App.Data.Abstract;
using App.Data.Entity.BaseEntities;

namespace App.Service.Abstract
{
    public interface IService<T> : IRepository<T> where T : class, IEntity, new()
    {

    }
}
