using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entity.BaseEntities
{
    public abstract class BaseEntity : IEntity
    {
        [Key] // Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // n = son kayıt (n + 1) otomatik artıracaktır.
        public int Id { get; set; }
    }
}
