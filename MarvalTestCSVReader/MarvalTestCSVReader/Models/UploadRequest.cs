using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MarvalTestCSVReader.Models
{
    public class UploadRequest
    {
        public IFormFile FileRef { get; private set; }
        public string FileName { get; private set; }
        public long FileSize { get; private set; }
        public string FileExtension { get; private set; }

        public static UploadRequest FromRequestForSingle(HttpRequest request)
        {
            var file = request.Form.Files.FirstOrDefault();
            if (file == null) throw new Exception("No file uploaded");

            return new UploadRequest
            {
                FileRef = file,
                FileName = file.FileName.Split('.')[0],
                FileSize = file.Length,
                FileExtension = Path.GetExtension(file.FileName)
                                    .Replace(".", string.Empty)
                                    .ToLower(),
            };
        }
    }
}
