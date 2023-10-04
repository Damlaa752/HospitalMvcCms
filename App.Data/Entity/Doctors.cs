using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entity
{
    public class Doctors : User
    {
        public string Specialty { get; set; }

        public List<Patient>? Patients { get; set; }

        public int? DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public Department? Department { get; set; }
    }
}
