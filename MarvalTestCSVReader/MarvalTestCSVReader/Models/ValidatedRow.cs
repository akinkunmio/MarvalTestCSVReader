﻿using MarvalTestCSVReader.Models.Core;
using System.Collections.Generic;

namespace MarvalTestCSVReader.Models
{
    public class ValidatedRow
    {
        public int Row { get; set; }
        public bool IsValid { get; set; }
        public IList<string> ErrorMessages { get; set; }
        public string Description { get; set; }

        protected string GetColumnValue(List<Column> columns, int index, string defaultValue)
        {
            return columns.Count > index ? columns[index].Value : defaultValue;
        }
    }
}
