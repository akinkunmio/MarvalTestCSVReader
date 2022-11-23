using MarvalTestCSVReader.Models;
using MarvalTestCSVReader.Models.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarvalTestCSVReader.Helpers
{
    public interface IFileContentValidator<T, TContext> where T : ValidatedRow
    {
        Task<IList<PersonRow>> Validate(IEnumerable<Row> rows, PersonContext context, bool hasHeader);
    }
}