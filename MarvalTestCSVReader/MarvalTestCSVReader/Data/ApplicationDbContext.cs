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
    }
}
