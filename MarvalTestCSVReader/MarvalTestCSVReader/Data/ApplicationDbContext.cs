using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarvalTestCSVReader.Models.DomainClasses;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarvalTestCSVReader.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);


            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
              new Person { Identity = 1, Active = "True", Age = "23", FirstName = "Chinua", Surname = "Achebe", IsValid = "True", Mobile = "07082018781", Sex = "M" },
              new Person { Identity = 2, Active = "True", Age = "30", FirstName = "Akin", Surname = "Tunde", IsValid = "True", Mobile = "09012213243", Sex = "M" });
        }

    }
    
}
