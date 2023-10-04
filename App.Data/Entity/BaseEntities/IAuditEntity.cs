using App.Data.Entity.BaseEntities;

namespace App.Data.Entity
{
    public interface IAuditEntity : IEntity
    {
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
