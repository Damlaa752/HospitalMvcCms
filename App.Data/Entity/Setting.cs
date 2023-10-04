using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Entity.BaseEntities;

namespace App.Data.Entity
{
    public class Setting : BaseEntity
    {

        [MaxLength(200, ErrorMessage = "The {0} cannot exceed 200 characters."), MinLength(2, ErrorMessage = "The {0} must be at least 2 characters."), Required(ErrorMessage = "The {0} field cannot be left blank!"), Column(TypeName = "nvarchar(200)")] // Error Message kısımları işlevsel olarak düzenlenerek eklenebilir.
        public string Name { get; set; }


        [Required(ErrorMessage = "The {0} field cannot be left blank!"), DataType(DataType.EmailAddress), EmailAddress, Column(TypeName = "varchar(200)"), MaxLength(200, ErrorMessage = "The {0} cannot exceed 200 characters."), MinLength(5, ErrorMessage = "The {0} must be at least 5 characters.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "The {0} field cannot be left blank!"), DataType(DataType.PhoneNumber), Column(TypeName = "nvarchar(20)"), MaxLength(20, ErrorMessage = "The {0} cannot exceed 20 characters."), MinLength(10, ErrorMessage = "The {0} must be at least 10 characters.")]
        public string Phone { get; set; }


        [MaxLength(400, ErrorMessage = "The {0} cannot exceed 400 characters."), MinLength(2, ErrorMessage = "The {0} must be at least 2 characters."), Required(ErrorMessage = "The {0} field cannot be left blank!"), Column(TypeName = "nvarchar(400)")]
        public string Address { get; set; }


        [MaxLength(400, ErrorMessage = "The {0} cannot exceed 400 characters."), MinLength(2, ErrorMessage = "The {0} must be at least 2 characters."), Required(ErrorMessage = "The {0} field cannot be left blank!"), Column(TypeName = "nvarchar(400)")]
        public string AlpLinkedin{ get; set; }


        [MaxLength(400, ErrorMessage = "The {0} cannot exceed 400 characters."), MinLength(2, ErrorMessage = "The {0} must be at least 2 characters."), Required(ErrorMessage = "The {0} field cannot be left blank!"), Column(TypeName = "nvarchar(400)")]
        public string KadirLinkedin{ get; set; }


        [MaxLength(400, ErrorMessage = "The {0} cannot exceed 400 characters."), MinLength(2, ErrorMessage = "The {0} must be at least 2 characters."), Required(ErrorMessage = "The {0} field cannot be left blank!"), Column(TypeName = "nvarchar(400)")]
        public string UtkuLinkedin{ get; set; }


        [MaxLength(75), DataType(DataType.ImageUrl), Required(ErrorMessage = "The {0} field cannot be left blank!")]
        public string Image { get; set; }

        public bool IsActive { get; set; }
    }
}
