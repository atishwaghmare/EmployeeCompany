using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeCompany.Data;
using EmployeeCompany.Models;
using EmployeeCompany.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCompany.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public CompanyController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllCompanies()
        {
            var allCompanies = dbContext.Companys.Include(c => c.Employees).ToList();
            return Ok(allCompanies);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOneCompany(int id)
        {
            var company = dbContext.Companys.Include(c => c.Employees).FirstOrDefault(c => c.CompanyId == id);
            if (company is null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        [HttpPost]
        public IActionResult AddCompany(AddCompanyDto addCompanyDto)
        {
            var companyEntity = new Company()
            {
                CompanyName = addCompanyDto.CompanyName
            };

            dbContext.Companys.Add(companyEntity);
            dbContext.SaveChanges();
            return Ok(companyEntity);
        }

        [HttpPut]
        public IActionResult UpdateCompany(int id, UpdateCompanyDto updateCompanyDto)
        {
            var company = dbContext.Companys.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            company.CompanyName = updateCompanyDto.CompanyName;
            dbContext.SaveChanges();
            return Ok(company);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteCompany(int id)
        {
            var company = dbContext.Companys.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            dbContext.Companys.Remove(company);
            dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("by-name/{name}")]
        public IActionResult GetCompanyByName(string name)
        {
            var company = dbContext.Companys.Include(c => c.Employees).FirstOrDefault(c => c.CompanyName == name);

            if (company == null)
            {
                return NotFound("Company name not found");
            }

            return Ok(company);
        }
    }
}
