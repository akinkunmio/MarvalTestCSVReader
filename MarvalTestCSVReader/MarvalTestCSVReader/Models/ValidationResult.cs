﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvalTestCSVReader.Models
{
    public class ValidationResult<T> where T : ValidatedRow
    {
        public ValidationResult()
        {
            ValidRows = new List<T>();
            Failures = new List<T>();
        }
        public IList<T> ValidRows { get; set; }

        public IList<T> Failures { get; set; }
    }
}