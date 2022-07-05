using MarvalTestCSVReader.Data;
using MarvalTestCSVReader.Helpers;
using MarvalTestCSVReader.Models;
using MarvalTestCSVReader.Models.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MarvalTestCSVReader.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFileReader _fileReader;
        private readonly IFileContentValidator<PersonRow, PersonContext> _fileContentValidator;
        private readonly ApplicationDbContext _dbContext;


        public HomeController(ILogger<HomeController> logger, IFileReader fileReader, ApplicationDbContext dbContext, IFileContentValidator<PersonRow, PersonContext> fileContentValidator)
        {
            _logger = logger;
            _fileReader = fileReader;
            _dbContext = dbContext;
            _fileContentValidator = fileContentValidator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(List<IFormFile> postedFiles)
        {
            var request = UploadRequest.FromRequestForSingle(Request);

            if (_fileReader.CanRead(request.FileExtension))
            {
                using (var contentStream = request.FileRef.OpenReadStream())
                {
                    IEnumerable<Row> rows = _fileReader.Read(contentStream);
                    var personRows  = await _fileContentValidator.Validate(rows, new PersonContext());
                    ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", request.FileName);
                }
            }
            else
            {
                throw new Exception("File extension not supported!.");
            }

         
            return View();
        }
    }
}