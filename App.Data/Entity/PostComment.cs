using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Entity.BaseEntities;

namespace App.Data.Entity
{
    public class PostComment : BaseAuditEntity
    {




        [ForeignKey(nameof(PostId))]
        public int? PostId { get; set; }
        public Post? Post { get; set; }


        [ForeignKey("User")]
        public int? UserId { get; set; }
        public User? User { get; set; }


        [Column(TypeName = "text"), DataType(DataType.Text)]
        public string Comment { get; set; }

        public bool IsActive { get; set; }

    }
}
