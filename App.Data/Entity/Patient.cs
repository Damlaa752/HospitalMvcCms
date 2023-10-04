using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entity
{
    public class Patient : User
    {

        public string? Diagnosis { get; set; }

        public Triage? TriageCode { get; set; }

        public int? DoctorId { get; set; }


        [ForeignKey(nameof(DoctorId))]
        public Doctors? Doctor { get; set; }


        public enum Triage
        {
            Empty, Black, Green, Yellow, Red
        }
    }
}
