using EmployeeCrudOperation_27_07.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCrudOperation_27_07.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext _context;

        public EmployeeController(EmployeeDbContext employeeDbContext)
        {
            _context = employeeDbContext;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmployee(Employee employee)
        {
            try
            {
                
                 _context.Employee.Add(employee);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return RedirectToAction(nameof(CreateNewEmployee));

            }
           
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employee.ToListAsync();
            return View(employees);
        }
        [HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string SearchQuery)
        {
           // string SearchQuery = Request.Form["SearchQuery"];
            if (SearchQuery == null)
            {
                return View();
            }
            var employees = await _context.Employee.Where(e => e.FirstName == SearchQuery || e.LastName == SearchQuery || e.City == SearchQuery || e.Country == SearchQuery).ToListAsync();
            if(employees == null)
            {
                return NotFound();
            }
            return View(employees); 


        }
        public IActionResult CreateNewEmployee()
        {
            return View();
        }
        [HttpGet("id")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                var DeleteEmployee=_context.Employee.FirstOrDefault(Emp=>Emp.Id == id);  
                if (DeleteEmployee != null)
                {
                    _context.Employee.Remove(DeleteEmployee);   
                    _context.SaveChanges(); 
                }
                
            }
            catch
            {
                return RedirectToAction(nameof(Index));


            }
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public IActionResult UpdateEmployee(int Id)
        {
            try
            {
                var updateEmployee=_context.Employee.FirstOrDefault(Emp=>Emp.Id == Id);
                if (updateEmployee != null)
                {
                    return View(updateEmployee);
                }

            }
            catch { 
                return RedirectToAction(nameof(UpdateEmployee));
            }

            return RedirectToAction(nameof(UpdateEmployee));
        }
        [HttpPost]
        public IActionResult UpdateEmployee(Employee employee)
        {
            try
            {

                _context.Employee.Update(employee);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return RedirectToAction(nameof(UpdateEmployee));    

            }
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var getemp=_context.Employee.First(Employee=>Employee.Id == id);
            return View(getemp);
        }
    }
}
