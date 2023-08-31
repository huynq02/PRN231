using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Q1.DTO;
using Q1.Models;

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public readonly PRN_Sum22_B1Context _context;
        public readonly IMapper _mapper;
        public CustomerController(PRN_Sum22_B1Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //1.3
        [HttpPost("customer/delete/{CustomerId}")]
        public ActionResult PostDeleteComtomer(string CustomerId) 
        {
            try
            {
                var customers = _context.Customers.Include(x =>x.Orders)
                    .ThenInclude(x =>x.OrderDetails)
                    .FirstOrDefault(x=>x.CustomerId == CustomerId);
                if (customers == null)
                {
                    return NotFound();
                }
                var countDelete = new ReturnDelete();
                countDelete.customerDeletedCount = 1;
                countDelete.orderDeletedCount = 0;
                countDelete.orderDetailDeletedCount = 0;
                foreach (var item in customers.Orders) 
                {
                    countDelete.orderDeletedCount++;

                    foreach (var o in item.OrderDetails) 
                    {
                        countDelete.orderDetailDeletedCount++;
                        _context.OrderDetails.Remove(o);
                    }
                    _context.Orders.Remove(item);
                }
                _context.Customers.Remove(customers);
                _context.SaveChanges();
                return Ok(countDelete);
            }
            catch (Exception ex) 
            {
                return BadRequest("There was an unknow error when performing data deletion");
            }
           
        }
    }
}

public class ReturnDelete 
{
    public int customerDeletedCount { get; set; }
    public int orderDeletedCount { get; set; }
    public int orderDetailDeletedCount { get; set; }
}