using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using MarvalTestCSVReader.Data;
using System.Collections.Generic;
using MarvalTestCSVReader.Models.DomainClasses;
using MarvalTestCSVReader.Helpers;
using MarvalTestCSVReader.Models.Core;
using MarvalTestCSVReader.Models;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarvalTestCSVReader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFileReader _fileReader;
        private readonly IFileContentValidator<PersonRow, PersonContext> _fileContentValidator;
        private readonly ApplicationDbContext _dbContext;


        public PersonsController(ILogger<HomeController> logger, IFileReader fileReader, ApplicationDbContext dbContext, IFileContentValidator<PersonRow, PersonContext> fileContentValidator)
        {
            _logger = logger;
            _fileReader = fileReader;
            _dbContext = dbContext;
            _fileContentValidator = fileContentValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPersons([FromQuery] string draw = default, [FromQuery] string start = default, [FromQuery] string length = default)
        {
            dynamic jsonData = default;

            try
            {
                int recordsTotal = 0;

                IEnumerable<Person> persons = await _dbContext.Persons.ToListAsync();

                var searchValue = HttpContext.Request.Query["search[value]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 10;

                int skip = start != null ? Convert.ToInt32(start) : 0;

                if (!string.IsNullOrEmpty(searchValue))
                {
                    persons = persons.Where(m => m.FirstName.Contains(searchValue) || m.Surname.Contains(searchValue))
                        .OrderBy(s => s.Identity);
                }

                recordsTotal = persons.Count();

                var data = persons.Skip(skip).Take(pageSize).Select(b => b);

                jsonData = new
                {
                    draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal,
                    data
                };

            }
            catch (Exception)
            {
                BadRequest("An error occured while fetching book. Retry.");
            }

            return Ok(jsonData);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int Id)
        {
            var person = new Person();

            try
            {
                if (Id == 0)
                {
                    throw new ArgumentException("Invalid Person Id.");
                }

                person = await _dbContext.Persons.SingleOrDefaultAsync(a => a.Identity == Id);

                if (person == null)
                {
                    BadRequest("Person not found!.");
                }

                _dbContext.Remove(person);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                return BadRequest($"An error occured {exception.Message} -- {exception.InnerException}");
            }

            return Ok(person);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> PostRentalsAsync(Person p)
        {
            try
            {
                var person = await _dbContext.Persons.SingleOrDefaultAsync(a => a.Identity == p.Identity);

                if (person == null) return BadRequest("User does not exist");

                    person.Surname = p.Surname;
                    person.FirstName = p.FirstName;
                    person.Age = p.Age;
                    person.Active = p.Active;
                    person.Mobile = p.Mobile;
                    person.Sex = p.Sex;

                _dbContext.Entry(person).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            
                    var response = new ValidationResult<PersonRow>();
            try
            {
                var request = UploadRequest.FromRequestForSingle(Request);

                if (_fileReader.CanRead(request.FileExtension))
                {
                    using var contentStream = request.FileRef.OpenReadStream();
                    IEnumerable<Row> rows = _fileReader.Read(contentStream);
                    var personRows = await _fileContentValidator.Validate(rows, new PersonContext(), true);
                    var persons = personRows.Select(p => p.Person);
                    response.Failures = personRows.Where(a => !a.IsValid).ToList();
                    response.ValidRows = personRows.Where(a => a.IsValid).ToList();
                    await _dbContext.AddRangeAsync(persons);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                BadRequest($"An error occured. {ex.Message}-- {ex.InnerException} -- {ex.StackTrace}");
            }
           
            return Ok(response);
        }

    }
}
