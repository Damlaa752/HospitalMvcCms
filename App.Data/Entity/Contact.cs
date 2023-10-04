using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.Entity.BaseEntities;

namespace App.Data.Entity
{
    public class Contact : BaseAuditEntity
    {
        [DataType(DataType.Text), Column(TypeName = "nvarchar(100)"), MaxLength(200, ErrorMessage = "The {0} cannot exceed 200 characters."), MinLength(5, ErrorMessage = "The {0} must be at least 5 characters.")]
        public string FullName { get; set; } = "Guest";

        [Required(ErrorMessage = "The {0} field cannot be left blank!"), DataType(DataType.EmailAddress), EmailAddress, Column(TypeName = "varchar(200)"), MaxLength(200, ErrorMessage = "The {0} cannot exceed 200 characters."), MinLength(5, ErrorMessage = "The {0} must be at least 5 characters.")]
        public string Email { get; set; }


        [MaxLength(100, ErrorMessage = "The {0} cannot exceed 200 characters."), MinLength(2, ErrorMessage = "The {0} must be at least 1 characters."), Required(ErrorMessage = "The {0} field cannot be left blank!"), Column(TypeName = "nvarchar(100)")] // Eğer nvarchar'ı belirtmezsek database üzerinde varchar olarak gözükür.

        public string Title { get; set; }

        [Required(ErrorMessage = "The {0} field cannot be left blank!"), DataType(DataType.PhoneNumber), Column(TypeName = "nvarchar(20)"), MaxLength(20, ErrorMessage = "The {0} cannot exceed 20 characters."), MinLength(10, ErrorMessage = "The {0} must be at least 10 characters.")]
        public string Phone { get; set; }


        [DataType(DataType.MultilineText), MaxLength(300, ErrorMessage = "The {0} cannot exceed 300 characters."), Column(TypeName = "nvarchar(300)")]
        public string? Message { get; set; }
    }
}
