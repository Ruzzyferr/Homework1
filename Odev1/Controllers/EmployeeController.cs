using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Odev1.Models;
using System.Diagnostics;
using System.Linq;

namespace Odev1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly NorthwndContext _context;

        public EmployeeController(NorthwndContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var employees = _context.Employees.ToList();
            return View(employees);
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

        
        public IActionResult Orders(int id)
        {
    
            var orders = _context.Orders.Where(o => o.EmployeeId == id).ToList();
            return View(orders);
            
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            if (ModelState.IsValid)
            {
                
                _context.Employees.Add(employee);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Yeni çalışan başarıyla eklendi.";
                return RedirectToAction("Index"); // Index, çalışanların listelendiği sayfa.
            }

            return View(employee);
        }


    }
}

