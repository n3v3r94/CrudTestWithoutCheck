

namespace Demo.Controllers
{

    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Demo.Models;
    using Demo.Models.EmployeeModels;
    using Demo.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authorization.Infrastructure;
    using Microsoft.AspNetCore.Identity;

    public class HomeController : Controller
    {
        private readonly EmployeeDbContext db;

        public HomeController (EmployeeDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var emp = db.Employees.ToList();
            return View(emp);
        }

        [Authorize]
        public IActionResult Create(int ? id)
        {
          
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult Create(Employee emp)
        {
            db.Employees.Add(emp);
            db.SaveChanges();
            return Redirect("/");
        }

        public IActionResult Edit(int? id)
        {
            var emp = db.Employees.Find(id);

            return View(emp);
        }

        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            var empFromDb = db.Employees.Find(emp.Id);
            empFromDb.Name = emp.Name;
            empFromDb.FullName = emp.FullName;
            empFromDb.City = emp.City;
            empFromDb.Age = emp.Age;
            db.SaveChanges();
            return Redirect("/");
        }

        
        public IActionResult Delete(int ?  id)
        {

            var emp = db.Employees.Find(id);

            return View(emp);
        }

        [HttpPost]
        
        public IActionResult Delete(int id)
        {

            var emp = db.Employees.Find(id);

            db.Employees.Remove(emp);
            db.SaveChanges();

            return Redirect("/");
        }

        public IActionResult OrderByYear()
        {
            var order = db.Employees.OrderBy(a => a.Age).ToList();
            return View(order);

        }



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
