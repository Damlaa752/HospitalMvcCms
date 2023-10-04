using App.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentPost> DepartmentPosts { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PostComment> PostComments { get; set; }

        public DbSet<Setting> Settings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
