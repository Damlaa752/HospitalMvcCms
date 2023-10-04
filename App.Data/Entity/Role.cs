using System.ComponentModel.DataAnnotations;
using App.Data.Entity.BaseEntities;

namespace App.Data.Entity
{
    public class Role : BaseEntity
    {
        [MaxLength(50, ErrorMessage = "The {0} cannot exceed 100 characters"), Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }

}
