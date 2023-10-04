using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Entity.BaseEntities;

namespace App.Data.Entity
{
    public class Page : BaseAuditEntity
    {

        [MaxLength(200, ErrorMessage = "The {0} cannot exceed 200 characters."), MinLength(1, ErrorMessage = "The {0} must be at least 1 characters."), Required(ErrorMessage = "The {0} field cannot be left blank!"), Column(TypeName = "nvarchar(200)")]
        public string Title { get; set; }


        [DataType(DataType.MultilineText), Required(ErrorMessage = "The {0} field cannot be left blank!"), Column(TypeName = "text")]
        public string Content { get; set; }


        public bool IsActive { get; set; }
    }
}
