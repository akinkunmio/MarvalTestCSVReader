using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvalTestCSVReader.Models.DomainClasses
{
    public class Person
    {
        public int Identity { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public decimal Mobile { get; set; }
        public bool Active { get; set; }
    }
}
