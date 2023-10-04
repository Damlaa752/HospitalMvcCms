using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Entity.BaseEntities;

namespace App.Data.Entity
{
    public class Post : BaseAuditEntity // Bunu News gibi işlerde kullanabiliyoruz Ancak poliklinikler için Departman Post ile Include ediyoruz.
    {

        [MaxLength(75), DataType(DataType.ImageUrl)]
        public string? Image { get; set; }

        [MaxLength(200, ErrorMessage = "The {0} cannot exceed 200 characters."), MinLength(1, ErrorMessage = "The {0} must be at least 1 characters."), Required(ErrorMessage = "The {0} field cannot be left blank!"), Column(TypeName = "nvarchar(200)")]
        public string Title { get; set; }

        [Column(TypeName = "ntext"), DataType(DataType.Text)]
        public string Content { get; set; }
        public int CommentsCount { get; set; }
        public List<PostComment>? Comments { get; set; }
    }
}
