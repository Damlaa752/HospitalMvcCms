using App.Data.Entity;

namespace App.Web.Mvc.Models
{
    public class HomePageViewModels
    {
        public Setting? Setting { get; set; }

        public Post? Post { get; set; }

        public Appointment? Appointment { get; set; }

        public Contact? Contact { get; set; }



    }
}
