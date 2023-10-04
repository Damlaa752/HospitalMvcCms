using App.Data.Entity;

namespace App.Web.Mvc.Models
{
    public class ContactViewModel
    {
        // Contact View'ı için hazırlanmıştır.

        public Setting? Setting { get; set; }

        public Contact? Contact { get; set; }
    }
}
