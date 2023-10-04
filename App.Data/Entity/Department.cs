using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Entity.BaseEntities;

namespace App.Data.Entity
{
    public class Department : BaseEntity
    {
        // IAuditEntity Base olarak değiştirildi. 

        [MaxLength(100, ErrorMessage = "The {0} cannot exceed 200 characters."), MinLength(2, ErrorMessage = "The {0} must be at least 1 characters."), Required(ErrorMessage = "The {0} field cannot be left blank!"), Column(TypeName = "nvarchar(100)")] // Eğer nvarchar'ı belirtmezsek database üzerinde varchar olarak gözükür.

        public string Name { get; set; }

        [MinLength(1, ErrorMessage = "The {0} must be at least 1 character.")]
        [Required(ErrorMessage = "The {0} field cannot be left blank!")]
        [Column(TypeName = "nvarchar(MAX)")]

        public string Description { get; set; }

        [MaxLength(75), DataType(DataType.ImageUrl)]
        public string? Image { get; set; }

        public List<Doctors>? Doctors { get; set; }
    }
}
