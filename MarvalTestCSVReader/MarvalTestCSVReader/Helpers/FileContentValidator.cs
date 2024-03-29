﻿using MarvalTestCSVReader.Models;
using MarvalTestCSVReader.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvalTestCSVReader.Helpers
{
    public class FileContentValidator : IFileContentValidator<PersonRow, PersonContext>
    {
        public async Task<IList<PersonRow>> Validate(IEnumerable<Row> rows, PersonContext context, bool hasHeader)
        {
            await Task.CompletedTask;

            if (hasHeader)
                rows = rows.Skip(1);

            var processedRows = new List<PersonRow>();
            var validationConfig = context.Configuration ?? new PersonRowConfig();

            foreach (Row row in rows)
            {
                processedRows.Add(new PersonRow(row, validationConfig));
            }

            return processedRows;
        }
    }
}
