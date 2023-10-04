using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Entity.BaseEntities;

namespace App.Data.Entity
{
    public class User : BaseAuditEntity
    {
        public int? RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role? Role { get; set; }

        [MaxLength(75), DataType(DataType.ImageUrl)]
        public string? Image { get; set; }

        [DataType(DataType.Text), Column(TypeName = "nvarchar(100)"), MaxLength(200, ErrorMessage = "The {0} cannot exceed 200 characters."), MinLength(5, ErrorMessage = "The {0} must be at least 5 characters.")]
        public string FullName { get; set; } = "Guest";

        [Required(ErrorMessage = "The {0} field cannot be left blank!"), DataType(DataType.EmailAddress), EmailAddress, Column(TypeName = "varchar(200)"), MaxLength(200, ErrorMessage = "The {0} cannot exceed 200 characters."), MinLength(5, ErrorMessage = "The {0} must be at least 5 characters.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "The {0} field cannot be left blank!"), DataType(DataType.Password), Column(TypeName = "nvarchar(100)"), MaxLength(100, ErrorMessage = "The {0} cannot exceed 100 characters."), MinLength(5, ErrorMessage = "The {0} must be at least 5 characters.")]
        public string Password { get; set; }


        [Required(ErrorMessage = "The {0} field cannot be left blank!"), Column(TypeName = "nvarchar(100)"), MaxLength(100, ErrorMessage = "The {0} cannot exceed 100 characters."), MinLength(2, ErrorMessage = "The {0} must be at least 2 characters.")]
        public string City { get; set; }


        [Required(ErrorMessage = "The {0} field cannot be left blank!"), DataType(DataType.PhoneNumber), Column(TypeName = "nvarchar(20)"), MaxLength(20, ErrorMessage = "The {0} cannot exceed 20 characters."), MinLength(10, ErrorMessage = "The {0} must be at least 10 characters.")]
        public string Phone { get; set; }
    }
}
