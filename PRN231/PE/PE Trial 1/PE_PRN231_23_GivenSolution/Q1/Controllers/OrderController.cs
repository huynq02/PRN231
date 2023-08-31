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
    public class OrderController : ControllerBase
    {
        private readonly PRN_Sum22_B1Context _context;
        private readonly IMapper _mapper;

        public OrderController(PRN_Sum22_B1Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //map = automapper
        [HttpGet("getallorder")]
        public ActionResult Get()
        {
            var order = _context.Orders.Include(x => x.Customer).Include(x => x.Employee).ThenInclude(x => x.Department).ToList();
            var mapper = _mapper.Map<List<OrderDTO>>(order);
            return Ok(mapper);
        }
        //map chay
        //[HttpGet("getallwithoutautomapper")]
        //public ActionResult GetOrder()
        //{
        //    var order = _context.Orders.Include(x => x.Customer).Include(x => x.Employee).ThenInclude(x => x.Department).ToList();
        //    var orderDto = new List<OrderDTO>();
        //    foreach (var item in order)
        //    {
        //        orderDto.Add(new OrderDTO()
        //        {
        //            customerId = item.CustomerId,
        //            customerName = item.Customer?.CompanyName,
        //            employeeId = item.EmployeeId,
        //            employeeName = item.Employee?.FirstName + ' ' + item.Employee?.LastName,
        //            employeeDepartmentId = item.Employee?.DepartmentId,
        //            employeeDepartmentName = item.Employee?.Department?.DepartmentName,
        //            orderDate = item.OrderDate,
        //            requiredDate = item.RequiredDate,
        //            shippedDate = item.ShippedDate,
        //            freight = item.Freight,
        //            shipName = item.ShipName,
        //            shipAddress = item.ShipAddress,
        //            shipCity = item.ShipCity,
        //            shipRegion = item.ShipRegion,
        //            shipPostalCode = item.ShipPostalCode,
        //            shipCountry = item.ShipCountry,
        //        });
        //    }
        //    return Ok(orderDto);
        //}
        //1.2
        [HttpGet("getorderbydate/{From}/{To}")]
        public ActionResult Getbydate(DateTime From, DateTime To)
        {
            var order = _context.Orders.Include(x => x.Customer)
                .Include(x => x.Employee)
                .ThenInclude(x => x.Department)
                .Where(x => x.OrderDate >= From && x.OrderDate <= To)
                .ToList();
            var mapper = _mapper.Map<List<OrderDTO>>(order);
            return Ok(mapper);
        }
        //1.3
        [HttpPost("customer/delete")]
        public ActionResult Post() 
        {
            return Ok();
        }
    }
}
