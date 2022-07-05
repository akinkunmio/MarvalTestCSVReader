using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvalTestCSVReader.Models.Core
{
    public class Row
    {
        public List<Column> Columns { get; set; }
        public int Index { get; set; }

        public bool IsValid { get; set; }
        public bool Messages { get; set; }
    }
}
