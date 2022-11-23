using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using MarvalTestCSVReader.Models.DomainClasses;
using MarvalTestCSVReader.Data;

namespace MarvalTestCSVReader.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public PeopleController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: PeopleController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PeopleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PeopleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PeopleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PeopleController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var p = await GetPerson(id);
            return View("Edit", p);
        }

        public async Task<Person> GetPerson(int id)
        {
            try
            {
                var person = await _dbContext.Persons.SingleOrDefaultAsync(a => a.Identity == id);
                if (person == null)
                    return new Person();
                return person;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: PeopleController/Edit/5
        [HttpPost]
        public async Task<ActionResult> EditAsync(Person p)
        {
            try
            {
                var person = await _dbContext.Persons.SingleOrDefaultAsync(a => a.Identity == p.Identity);

                if (person == null) return View(); ;

                person.Surname = p.Surname;
                person.FirstName = p.FirstName;
                person.Age = p.Age;
                person.Active = p.Active;
                person.Mobile = p.Mobile;
                person.Sex = p.Sex;

                _dbContext.Entry(person).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            catch
            {
                return View();
            }
        }
      
    }
}
