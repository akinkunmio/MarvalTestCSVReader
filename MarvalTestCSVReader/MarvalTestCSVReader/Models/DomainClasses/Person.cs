using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarvalTestCSVReader.Models.DomainClasses
{
    public class Person
    {
        [Key]
        public int Identity { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string Mobile { get; set; }
        public string Active { get; set; }
        public string IsValid { get; set; }
    }
}
