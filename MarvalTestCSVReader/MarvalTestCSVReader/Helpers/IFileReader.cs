using MarvalTestCSVReader.Models.Core;
using System.Collections.Generic;
using System.IO;

namespace MarvalTestCSVReader.Helpers
{
    public interface IFileReader
    {
        bool CanRead(string fileExtension);
        IEnumerable<Row> Read(Stream stream);
    }
}