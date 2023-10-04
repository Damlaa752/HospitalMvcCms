using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Entity.BaseEntities;

namespace App.Data.Entity
{
    public class DepartmentPost : BaseEntity
    {

        public int? DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public Department? Departments { get; set; }

        public int? PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        public Post? Post { get; set; }

    }
}
