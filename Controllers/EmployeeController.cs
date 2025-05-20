using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeCompany.Data;
using EmployeeCompany.Models;
using EmployeeCompany.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using EmployeeCompany.Services;

namespace EmployeeCompany.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        //read or store real time entry in file using filelogger
        private readonly FileLogger logger;


        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllEmployee()
        {
//I did change here, I added include method 
            var allEmployees = dbContext.Employees.Include(e => e.Company).ToList();
            return Ok(allEmployees);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOneEmployee(int id)
        {
            var emp = dbContext.Employees.Find(id);
            if (emp is null)
            {
                return NotFound();
            }
            return Ok(emp);
        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto, FileLogger logger)
        {
            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary,
                CompanyId = addEmployeeDto.CompanyId
            };

            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();

            logger.Log($"Added Employee: ID={employeeEntity.EmployeeId}, Name={employeeEntity.Name}");
            return Ok(employeeEntity);
        }

        [HttpPut]
        public IActionResult UpdateEmployee(int id, UpdateEmployeeDto updateEmployeeDto)
        {
            var emp = dbContext.Employees.Find(id);
            if (emp == null)
            {
                return NotFound();
            }

            emp.Name = updateEmployeeDto.Name;
            emp.Phone = updateEmployeeDto.Phone;
            emp.Salary = updateEmployeeDto.Salary;
            emp.CompanyId = updateEmployeeDto.CompanyId;

            dbContext.SaveChanges();
            return Ok(emp);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteEmployee(int id, FileLogger logger)
        {
            var emp = dbContext.Employees.Find(id);
            if (emp == null)
            {
                return NotFound();
            }

            dbContext.Employees.Remove(emp);
            dbContext.SaveChanges();
            logger.Log($"Deleted Employee: ID={emp.EmployeeId}, Name={emp.Name}");
            return Ok();
        }

        [HttpGet]
        [Route("by-name/{name}")]
        public IActionResult GetEmployeeByName(string name)
        {
            var emp = dbContext.Employees.Include(e => e.Company).FirstOrDefault(e => e.Name == name);

            if (emp == null)
            {
                return NotFound("Employee not found by name");
            }
            return Ok(emp);
        }

        [HttpGet]
        [Route("by-phone/{phone}")]
        public IActionResult GetEmployeeByPhone(string phone)
        {
            var emp = dbContext.Employees.Include(e => e.Company).FirstOrDefault(e => e.Phone == phone);

            if (emp == null)
            {
                return NotFound("Employee not found by phone");
            }
            return Ok(emp);
        }

        [HttpGet("paged")]

        public IActionResult GetPagedEmployees(int pageNumber = 2, int pageSize = 2)
        {
            var totalRecords = dbContext.Employees.Count();

            var data = dbContext.Employees
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();

            

            return Ok(data);
        }

        //search by  id or name or  phone use any one

        [HttpGet("search")]
        public IActionResult SearchEmployee(string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Please provide a search term (ID, Name, or Phone).");
            }

            var employees = dbContext.Employees
                .Where(e =>
                    e.EmployeeId.ToString() == searchTerm ||               // Match by Employee ID
                    e.Name.Contains(searchTerm) ||                         // Match by Name
                    e.Phone.Contains(searchTerm))                          // Match by Phone
                .ToList();

            if (employees == null || employees.Count == 0)
            {
                return NotFound("No employee found with the given search term.");
            }

            return Ok(employees);
        }

    }
}
