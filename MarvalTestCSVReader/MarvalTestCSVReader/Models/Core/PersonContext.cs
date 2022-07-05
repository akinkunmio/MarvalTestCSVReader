using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvalTestCSVReader.Models.Core
{
    public class PersonContext
    {
        public PersonContext()
        {
            Configuration = new PersonRowConfig();
        }
        public PersonRowConfig Configuration { get; set; }
        public string Identity { get; set; }
    }
}
