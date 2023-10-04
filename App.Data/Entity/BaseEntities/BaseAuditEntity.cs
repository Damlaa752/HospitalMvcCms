using App.Data.Entity.BaseEntities;

namespace App.Data.Entity
{
    public abstract class BaseAuditEntity : BaseEntity, IAuditEntity
    {

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Bir kere oluşturulduktan sonra sabit kalmasını istiyoruz.
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow; // Her güncellemede son değeri almasını istiyoruz.
        public DateTime? DeletedAt { get; set; }
    }
}
