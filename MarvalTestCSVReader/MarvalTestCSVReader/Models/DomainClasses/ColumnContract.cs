using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvalTestCSVReader.Models
{
    public class ColumnContract
    {
        public bool? Required { get; set; }

        public string ColumnName { get; set; }

        public string DataType { get; set; }

        public int? Min { get; set; }

        public int? Max { get; set; }

        public bool? ValidateCell { get; set; }
    }
}
