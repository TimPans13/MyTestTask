using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyTestTask.Models;

namespace MyTestTask.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser> ///: DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.EnsureCreated();

            //if (!Contacts.Any())
            //{
            //    Contacts.AddRange(
            //        new Contact { Name = "Ivan", MobilePhone = "+375332221100", JobTitle = "Senior", BirthDate = DateTime.Now.AddYears(-30) },
            //        new Contact { Name = "Lena", MobilePhone = "+375334441100", JobTitle = "Middle", BirthDate = DateTime.Now.AddYears(-25) }
            //    );

            //    SaveChanges();
            //}
        }


    }




}
