using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Q1_APIs.Models;
using System;

namespace Q1_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly PE_PRN_23SumContext _context;
        public EmployeeController(PE_PRN_23SumContext context) 
        {
            _context = context;
        }
        [HttpGet("Index")]
        [EnableQuery]// cau 4
        [Produces("application/json", "application/xml", "text/csv")]
        public ActionResult Index() 
        {
            var em = _context.Employees.Include(x =>x.ReportsToNavigation).Select(x => new 
            {
                employeeId = x.EmployeeId,
                lastName = x.LastName,
                firstName = x.FirstName,
                title = x.Title,
                titleOfCourtesy = x.TitleOfCourtesy,
                birthDate = x.BirthDate,
                birthDateStr = x.BirthDate.Value.ToString("MM/dd/yyyy"),
                reportsTo = x.ReportsToNavigation.FirstName + " " + x.LastName,
            });
            return Ok(em.AsQueryable());//cau 4
            //return Ok(em.ToList()); //cau 1
        }
        [HttpGet("list/{minyear}/{maxyear}")]
        [Produces("application/json", "application/xml", "text/csv")]
        public ActionResult list( int minyear, int maxyear  ) 
        {
            var em = _context.Employees.
                Include(x => x.ReportsToNavigation).
                Where(x => x.BirthDate.Value.Year >= minyear && x.BirthDate.Value.Year <= maxyear).// phải có ? ở DateTime? thì ms dk .Value khong co ? bo value
                Select(x => new
            {
                employeeId = x.EmployeeId,
                lastName = x.LastName,
                firstName = x.FirstName,
                title = x.Title,
                titleOfCourtesy = x.TitleOfCourtesy,
                birthDate = x.BirthDate,
                birthDateStr = x.BirthDate.Value.ToString("MM/dd/yyyy"),
                reportsTo = x.ReportsToNavigation.FirstName + " " + x.LastName,
            });
            return Ok(em.ToList());
        }
        [HttpDelete("delete/{id}")]
        public ActionResult delete(int id) 
        {
                Employee delete = _context.Employees.Find(id);
                if (delete == null)
                {
                    return NotFound("The requested employee could not be found");
                }
                else 
                {
                 _context.Employees.Remove(delete);
                _context.SaveChanges();
                }
            
            return Ok();
        }
        
    }
}
